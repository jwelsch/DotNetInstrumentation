using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetInstrumentation
{
    public static class Extensions
    {
        public static IApplicationBuilder UseInstrumentationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstrumentationMiddleware>();
        }

        public static bool SeekToBegining(this Stream stream)
        {
            if (stream.CanSeek)
            {
                return stream.Seek(0, SeekOrigin.Begin) == 0L;
            }

            return false;
        }
    }
}
