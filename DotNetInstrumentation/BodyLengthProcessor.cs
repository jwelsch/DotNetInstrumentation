using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DotNetInstrumentation
{
    public class BodyLengthProcessor : IProcessor
    {
        public long Minimum { get; private set; } = long.MaxValue;

        public long Maximum { get; private set; } = long.MinValue;

        public double Average { get; private set; }

        public long Total { get; private set; }

        public int Count { get; private set; }

        private Dictionary<string, Stream> originalBodies = new Dictionary<string, Stream>();

        public async Task StartAsync(HttpContext context)
        {
            //this.originalBodies.Add()
        }

        public async Task<IProcessorResult> StopAsync(HttpContext context)
        {
            var totalCount = 0L;

            var originalBody = context.Response.Body;

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            using (var memoryStream = new MemoryStream())
            {
                var bufferLength = 1000 * 1024;
                var buffer = new byte[bufferLength];
                var readCount = 0L;

                do
                {
                    readCount = await context.Response.Body.ReadAsync(buffer, 0, bufferLength);

                    totalCount += readCount;
                }
                while (readCount == bufferLength);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }

            if (totalCount < this.Minimum || this.Count == 0)
            {
                this.Minimum = totalCount;
            }

            if (totalCount > this.Maximum || this.Count == 0)
            {
                this.Maximum = totalCount;
            }

            this.Total += totalCount;
            this.Average = this.Total / ++this.Count;

            return new BodyLengthResult(this.Minimum, this.Maximum, this.Average);
        }
    }
}
