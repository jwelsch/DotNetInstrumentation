using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class RequestProcessorResult
    {
        public IBodyStatistics BodyStatistics
        {
            get;
        }

        public TimeSpan RequestInterval
        {
            get;
        }

        public RequestProcessorResult(IBodyStatistics bodyStatistics, TimeSpan requestInterval)
        {
            this.BodyStatistics = bodyStatistics;
            this.RequestInterval = requestInterval;
        }
    }
}
