using DotNetInstrumentation;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Test
{
    [TestClass]
    public class BodyLengthProcessorTests
    {
        [TestMethod]
        public void ResultWithOneRequest()
        {
            var processor = new BodyLengthProcessor();

            var context = new DefaultHttpContext();

            context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes("This is a sample body."));

            processor.StartAsync(context);

            var result = (BodyLengthResult) processor.StopAsync(context);

            Assert.AreEqual(22, result.Minimum);
            Assert.AreEqual(22, result.Maximum);
            Assert.AreEqual(22, result.Average);
        }

        [TestMethod]
        public void ResultWithOneRequestWithZeroLengthBody()
        {
            var processor = new BodyLengthProcessor();

            var context = new DefaultHttpContext();

            processor.StartAsync(context);

            var result = (BodyLengthResult)processor.StopAsync(context);

            Assert.AreEqual(0, result.Minimum);
            Assert.AreEqual(0, result.Maximum);
            Assert.AreEqual(0, result.Average);
        }

        [TestMethod]
        public void ResultWith100Requests()
        {
            var processor = new BodyLengthProcessor();
            var total = 0L;
            var average = 0d;
            var minimum = long.MaxValue;
            var maximum = long.MinValue;

            for (var i = 0; i < 100; i++)
            {
                var context = new DefaultHttpContext();

                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"This is a sample body {i}."));

                processor.StartAsync(context);

                var result = (BodyLengthResult)processor.StopAsync(context);

                total += context.Response.Body.Length;
                average = total / (i + 1);
                minimum = context.Response.Body.Length < minimum ? context.Response.Body.Length : minimum;
                maximum = context.Response.Body.Length > maximum ? context.Response.Body.Length : maximum;

                Assert.AreEqual(minimum, result.Minimum);
                Assert.AreEqual(maximum, result.Maximum);
                Assert.AreEqual(average, result.Average);
            }
        }

        [TestMethod]
        public void ResultWith100RequestsWhereOneHasZeroLengthBody()
        {
            var processor = new BodyLengthProcessor();
            var total = 0L;
            var average = 0d;
            var minimum = long.MaxValue;
            var maximum = long.MinValue;

            for (var i = 0; i < 100; i++)
            {
                var context = new DefaultHttpContext();

                if (i != 50)
                {
                    context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"This is a sample body {i}."));
                }

                processor.StartAsync(context);

                var result = (BodyLengthResult)processor.StopAsync(context);

                total += context.Response.Body.Length;
                average = total / (i + 1);
                minimum = context.Response.Body.Length < minimum ? context.Response.Body.Length : minimum;
                maximum = context.Response.Body.Length > maximum ? context.Response.Body.Length : maximum;

                Assert.AreEqual(minimum, result.Minimum);
                Assert.AreEqual(maximum, result.Maximum);
                Assert.AreEqual(average, result.Average);
            }
        }

        [TestMethod]
        public void DoesNotThrowExceptionWhenStartIsNotCalled()
        {
            var context = new DefaultHttpContext();

            var processor = new BodyLengthProcessor();

            var result = processor.StopAsync(context);

            Assert.IsFalse(result.IsError);
            Assert.IsNull(result.Error);
        }
    }
}
