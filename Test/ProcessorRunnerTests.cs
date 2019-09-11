using DotNetInstrumentation;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Test.TestClasses;

namespace Test
{
    [TestClass]
    public class ProcessorRunnerTests
    {
        [TestMethod]
        public void StartStopOnAllProcessorsAreCalled()
        {
            var display = new TestResultDisplay();
            var processors = new IProcessor[]
            {
                new TestProcessor(),
                new TestProcessor(),
                new TestProcessor()
            };

            var context = new DefaultHttpContext();

            var runner = new ProcessorRunner(processors);

            runner.Start(context);

            runner.Stop(context, display);

            foreach (TestProcessorResult result in display.Results)
            {
                Assert.IsTrue(result.WasStartCalled);
                Assert.IsTrue(result.WasStopCalled);
            }
        }
    }
}
