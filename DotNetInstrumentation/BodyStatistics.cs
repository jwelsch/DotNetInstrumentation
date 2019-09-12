using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class BodyStatistics
    {
        public long Minimum { get; private set; } = long.MaxValue;

        public long Maximum { get; private set; } = long.MinValue;

        public double Average { get; private set; }

        public long Total { get; private set; }

        public int Count { get; private set; }

        public BodyStatistics()
        {
        }

        public BodyStatistics(BodyStatistics statistics)
        {
            this.Minimum = statistics.Minimum;
            this.Maximum = statistics.Maximum;
            this.Average = statistics.Average;
            this.Total = statistics.Total;
            this.Count = statistics.Count;
        }

        public void AddRequest(long bodyLength)
        {
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
        }
    }
}
