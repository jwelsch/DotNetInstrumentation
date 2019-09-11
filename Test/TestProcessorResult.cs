using DotNetInstrumentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class TestProcessorResult : ProcessorResult
    {
        public TestProcessorResult(Exception error = null)
            : base(error)
        {
        }
    }
}
