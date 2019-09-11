using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetInstrumentation
{

    [Serializable]
    public class HttpContextNotFoundException : Exception
    {
        public HttpContextNotFoundException()
        {
        }

        public HttpContextNotFoundException(string message)
            : base(message)
        {
        }

        public HttpContextNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected HttpContextNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
