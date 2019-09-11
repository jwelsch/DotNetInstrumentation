using DotNetInstrumentation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestClasses
{
    public class TestProcessor : IProcessor
    {
        private bool wasStartCalled;

        public async Task StartAsync(HttpContext context)
        {
            this.wasStartCalled = true;
        }

        public async Task<IProcessorResult> StopAsync(HttpContext context)
        {
            return new TestProcessorResult(this.wasStartCalled, true);
        }
    }
}
