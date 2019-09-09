using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class BodyStatistics : IBodyStatistics
    {
        public long Minimum
        {
            get;
            private set;
        }

        public long Maximum
        {
            get;
            private set;
        }

        public double Average
        {
            get;
            private set;
        }

        public long Total
        {
            get;
            private set;
        }

        public int Count
        {
            get;
            private set;
        }

        public void Register(long bytes)
        {
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
        }
    }
}
