using DotNetInstrumentation;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class RequestTimerProcessorTests
    {
        [TestMethod]
        public void TimeNormalRequest()
        {
            var context = new DefaultHttpContext();

            var processor = new RequestTimerProcessor();

            processor.Start(context);

            System.Threading.Thread.Sleep(100);

            var result = (RequestTimerResult) processor.Stop(context);

            Assert.IsFalse(result.IsError);
            Assert.IsTrue(result.Interval.TotalMilliseconds > 100);
        }

        [TestMethod]
        public void ThrowsExceptionWhenStartIsNotCalled()
        {
            var context = new DefaultHttpContext();

            var processor = new RequestTimerProcessor();

            var result = processor.Stop(context);

            Assert.IsTrue(result.IsError);
            Assert.IsInstanceOfType(result.Error, typeof(HttpContextNotFoundException));
        }
    }
}
