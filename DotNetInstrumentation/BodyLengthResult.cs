using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class BodyLengthResult : ProcessorResult
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

        public BodyLengthResult(long minimum, long maximum, double average)
            : base()
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Average = average;
        }

        public BodyLengthResult(Exception error)
            : base(error)
        {
        }
    }
}
