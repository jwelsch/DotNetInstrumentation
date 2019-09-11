using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class RequestTimerResult : ProcessorResult
    {
        public TimeSpan Interval
        {
            get;
        }

        public RequestTimerResult(TimeSpan interval)
            : base()
        {
            this.Interval = interval;
        }

        public RequestTimerResult(Exception error)
            : base(error)
        {
        }
    }
}
