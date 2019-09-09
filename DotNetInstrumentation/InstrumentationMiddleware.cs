using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DotNetInstrumentation
{
    // Your IHttpModule(or Middleware) should be designed so that it can be safely added to any ASP.NET(or ASP.NET Core) web application.

    // Your project must satisfy the following requirements:
    
    //      Measure the total time spent processing the request.
    //      Measure the size of the response body in bytes. Calculate the minimum, average, and maximum responses seen so far.
    //      Add new content to HTML pages to display information gathered by your project.
    //      Ensure that your IHttpModule or Middleware is thread-safe and can correctly handle multiple concurrent requests.
    //      Handle multiple encodings and different types of pages.
    //      Use server resources (i.e.memory and processor) efficiently.
    //      Include a small web application that demonstrates the behavior of your IHttpModule(or Middleware.) This web application is not the focus of the project and you should feel free to use the web application template projects provided by Visual Studio.

    // In addition, please make sure to do the following:

    //      Integrate with a free CI Pipeline to compile, build, test and report in AppVeyor CI or Azure DevOps.
    //      Include unit and/or integration tests.
    //      Include an amazing README in your GitHub project that explains what you built, why you built it, how to deploy it up and how to use it. Include the AppVeyor or Azure DevOps build badge in your README to show status.
    //      Your project’s README should also outline future improvements you would like to make to the project.

    public class InstrumentationMiddleware
    {
        private readonly RequestDelegate _next;

        private object lockObj1 = new object();
        private object lockObj2 = new object();

        private RequestProcessor requestProcessor = new RequestProcessor();

        public InstrumentationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            lock (lockObj1)
            {
                context.TraceIdentifier = Guid.NewGuid().ToString();

                this.requestProcessor.Start(context.TraceIdentifier);
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);

            lock (lockObj2)
            {
                this.requestProcessor.Stop(context.TraceIdentifier, context.Response.Body.Length);
            }
        }
    }
}
