using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DotNetInstrumentation
{
    public class RequestTimerProcessor : IProcessor
    {
        private Dictionary<string, DateTime> startTimes = new Dictionary<string, DateTime>();

        public void Start(HttpContext context)
        {
            this.startTimes.Add(context.TraceIdentifier, DateTime.Now);
        }

        public IProcessorResult Stop(HttpContext context)
        {
            DateTime start;

            if (!this.startTimes.TryGetValue(context.TraceIdentifier, out start))
            {
                return new RequestTimerResult(new HttpContextNotFoundException($"Could not find start time for context {context.TraceIdentifier}"));
            }

            this.startTimes.Remove(context.TraceIdentifier);

            var interval = DateTime.Now - start;

            return new RequestTimerResult(interval);
        }
    }
}
