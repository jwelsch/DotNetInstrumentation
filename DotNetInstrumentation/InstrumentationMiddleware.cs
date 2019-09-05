using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DotNetInstrumentation
{
    public class InstrumentationMiddleware
    {
        private readonly RequestDelegate _next;

        public InstrumentationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
