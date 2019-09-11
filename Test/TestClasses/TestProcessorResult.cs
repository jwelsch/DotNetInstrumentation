using DotNetInstrumentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.TestClasses
{
    public class TestProcessorResult : ProcessorResult
    {
        public bool WasStartCalled { get; }

        public bool WasStopCalled { get; }

        public TestProcessorResult(bool wasStartCalled, bool wasStopCalled)
            : this(null)
        {
            this.WasStartCalled = wasStartCalled;
            this.WasStopCalled = wasStopCalled;
        }

        public TestProcessorResult(Exception error = null)
            : base(error)
        {
        }
    }
}
