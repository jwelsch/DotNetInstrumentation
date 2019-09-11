using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
        public long Minimum { get; private set; } = long.MaxValue;

        public long Maximum { get; private set; } = long.MinValue;

        public double Average { get; private set; }

        public long Total { get; private set; }

        public int Count { get; private set; }


        public InstrumentationMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var startTime = DateTime.Now;
            var bodyLength = 0L;
            var originalBody = context.Response.Body;

            try
            {
                // Need to change response body to a stream that has a length.
                using (var memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await next(context);

                    bodyLength = context.Response.Body.Length;

                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    var contentType = context.Response.Headers["Content-Type"];

                    await context.Response.Body.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

            if (bodyLength < this.Minimum || this.Count == 0)
            {
                this.Minimum = bodyLength;
            }

            if (bodyLength > this.Maximum || this.Count == 0)
            {
                this.Maximum = bodyLength;
            }

            this.Total += bodyLength;
            this.Average = this.Total / ++this.Count;

            var interval = DateTime.Now - startTime;

            System.Diagnostics.Trace.WriteLine($"Request path: {context.Request.Path}\n   Response time (ms): {interval.TotalMilliseconds}\n   Body length average: {this.Average}\n   Body length minimum: {this.Minimum}\n   Body length maximum: {this.Maximum}");
        }
    }
}
