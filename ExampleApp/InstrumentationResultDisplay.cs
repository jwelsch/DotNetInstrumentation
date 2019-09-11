using DotNetInstrumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class InstrumentationResultDisplay : IResultDisplay
    {
        public void Display(IProcessorResult result)
        {
            System.Diagnostics.Trace.WriteLine("InstrumentationResultDisplay.Display()");
        }
    }
}
