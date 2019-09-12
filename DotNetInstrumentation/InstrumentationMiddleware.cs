using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInstrumentation
{
    // Your IHttpModule(or Middleware) should be designed so that it can be safely added to any ASP.NET(or ASP.NET Core) web application.

    // Your project must satisfy the following requirements:

    //      - Measure the total time spent processing the request.
    //      - Measure the size of the response body in bytes. Calculate the minimum, average, and maximum responses seen so far.
    //      - Add new content to HTML pages to display information gathered by your project.
    //      - Ensure that your IHttpModule or Middleware is thread-safe and can correctly handle multiple concurrent requests.
    //      - Handle multiple encodings and different types of pages.
    //      - Use server resources (i.e.memory and processor) efficiently.
    //      - Include a small web application that demonstrates the behavior of your IHttpModule(or Middleware.) This web application is not the focus of the project and you should feel free to use the web application template projects provided by Visual Studio.

    // In addition, please make sure to do the following:

    //      - Integrate with a free CI Pipeline to compile, build, test and report in AppVeyor CI or Azure DevOps.
    //      - Include unit and/or integration tests.
    //      - Include an amazing README in your GitHub project that explains what you built, why you built it, how to deploy it up and how to use it. Include the AppVeyor or Azure DevOps build badge in your README to show status.
    //      - Your project’s README should also outline future improvements you would like to make to the project.

    public class InstrumentationMiddleware : IMiddleware
    {
        private object lockObj = new object();

        private BodyStatistics bodyStatistics = new BodyStatistics();

        public InstrumentationMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Record start time of request.
            var startTime = DateTime.Now;

            // Save a reference to the original body of the response.
            var originalBody = context.Response.Body;

            try
            {
                // Need to change response body to a stream that has a length.
                using (var memoryStream = new MemoryStream())
                {
                    // Swap the response body with a MemoryStream, which can read and seek.
                    context.Response.Body = memoryStream;

                    // Send request down pipeline.
                    await next(context);

                    // Get the content type of the response to determine encoding.
                    var contentType = new ContentType(context.Response.ContentType);

                    context.Response.Body.SeekToBegining();

                    BodyStatistics requestBodyStatistics;

                    lock (lockObj)
                    {
                        // Add the request data to the statistics data.
                        this.bodyStatistics.AddRequest(context.Response.Body.Length);

                        // Create a new variable with a copy of the body statistics for thread safety.
                        requestBodyStatistics = new BodyStatistics(this.bodyStatistics);
                    }

                    // Calculate the length of time the request took.
                    var interval = DateTime.Now - startTime;

                    // Write the gathered information to any HTML page.
                    await WriteContent(memoryStream, contentType, requestBodyStatistics, interval);

                    // Copy the data in the local MemoryStream to the original body.
                    await memoryStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                // Set the reference to the original body in the response object so it gets returned to the client.
                context.Response.Body = originalBody;
            }
        }

        private static async Task WriteContent(Stream stream, ContentType contentType, BodyStatistics bodyStatistics, TimeSpan interval)
        {
            // Only inject the gathered information into HTML pages.
            if (contentType.MediaType == "text/html")
            {
                // Get an encoding object for use later.
                var encoding = Encoding.GetEncoding(contentType.CharSet);

                stream.SeekToBegining();

                // Construct the HTML to be injected.
                var content = $"Time: {interval.TotalMilliseconds} ms<br/ >Body length minimum: {bodyStatistics.Minimum}<br/ >Body length maximum: {bodyStatistics.Maximum}<br/ >Body length average: {bodyStatistics.Average}";

                // Parse the HTML that is to be returned to the client.
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(stream);

                // Create a new HTML div element to hold the injected informatin.
                var div = document.CreateElement("div");
                div.InnerHtml = content;

                // Add the new HTML div to the document.
                document.Body.Prepend(div);

                stream.SeekToBegining();

                // Truncate the stream that will be sent to the client so as to not return anything that shouldn't.
                stream.SetLength(0);

                // Write the HTML with the injected information to the response stream.
                using (var writer = new StreamWriter(stream, encoding, 4096, true))
                {
                    await writer.WriteAsync(document.DocumentElement.OuterHtml);
                }

                stream.SeekToBegining();
            }
        }
    }
}
