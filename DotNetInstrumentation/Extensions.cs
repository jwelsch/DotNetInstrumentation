using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public static class Extensions
    {
        public static IApplicationBuilder UseInstrumentationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstrumentationMiddleware>();
        }
    }
}
