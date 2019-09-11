using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public interface IResultDisplay
    {
        void Display(IProcessorResult result);
    }
}
