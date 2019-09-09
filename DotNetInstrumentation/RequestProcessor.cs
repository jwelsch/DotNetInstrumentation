using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DotNetInstrumentation
{
    public class RequestProcessor
    {
        private Dictionary<string, DateTime> requests = new Dictionary<string, DateTime>();

        public BodyStatistics BodyStatistics
        {
            get;
        }

        public void Start(string id)
        {
            this.requests.Add(id, DateTime.UtcNow);
        }

        public RequestProcessorResult Stop(string id, long bodyLength)
        {
            this.BodyStatistics.Register(bodyLength);

            var interval = DateTime.UtcNow - this.requests[id];

            return new RequestProcessorResult(this.BodyStatistics, interval);
        }
    }
}
