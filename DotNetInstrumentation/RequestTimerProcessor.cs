using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DotNetInstrumentation
{
    public class RequestTimerProcessor : IProcessor
    {
        private DateTime start;

        public void Start(HttpContext context)
        {
            this.start = DateTime.Now;
        }

        public IProcessorResult Stop(HttpContext context)
        {
            var interval = DateTime.Now - this.start;

            return new RequestTimerResult(interval);
        }
    }
}
