using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class RequestTimerResult : IProcessorResult
    {
        public TimeSpan Interval
        {
            get;
        }

        public RequestTimerResult(TimeSpan interval)
        {
            this.Interval = interval;
        }
    }
}
