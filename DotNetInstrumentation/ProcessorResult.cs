using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public abstract class ProcessorResult : IProcessorResult
    {
        public bool IsError
        {
            get { return this.Error != null; }
        }

        public Exception Error { get; private set; }

        protected ProcessorResult(Exception error = null)
        {
            this.Error = error;
        }
    }
}
