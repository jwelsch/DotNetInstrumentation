using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public interface IProcessorResult
    {
        bool IsError { get; }

        Exception Error { get; }
    }
}
