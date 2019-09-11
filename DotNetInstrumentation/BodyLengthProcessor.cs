using System;
using System.Collections.Generic;
using System.Text;
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

        public void Start(HttpContext context)
        {
        }

        public IProcessorResult Stop(HttpContext context)
        {
            var bytes = context.Response.Body.Length;

            if (bytes < this.Minimum || this.Count == 0)
            {
                this.Minimum = bytes;
            }

            if (bytes > this.Maximum || this.Count == 0)
            {
                this.Maximum = bytes;
            }

            this.Total += bytes;
            this.Average = this.Total / ++this.Count;

            return new BodyLengthResult(this.Minimum, this.Maximum, this.Average);
        }
    }
}
