using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class ProcessorResultTests
    {
        [TestMethod]
        public void PassNullForErrorReportsNoError()
        {
            var result = new TestProcessorResult();

            Assert.IsFalse(result.IsError);
            Assert.IsNull(result.Error);
        }

        [TestMethod]
        public void PassNotNullForErrorReportsError()
        {
            var result = new TestProcessorResult(new Exception("Sample error message."));

            Assert.IsTrue(result.IsError);
            Assert.IsInstanceOfType(result.Error, typeof(Exception));
        }
    }
}
