using DotNetInstrumentation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.TestClasses
{
    public class TestProcessor : IProcessor
    {
        private bool wasStartCalled;

        public void Start(HttpContext context)
        {
            this.wasStartCalled = true;
        }

        public IProcessorResult Stop(HttpContext context)
        {
            return new TestProcessorResult(this.wasStartCalled, true);
        }
    }
}
