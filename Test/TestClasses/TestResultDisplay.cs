using DotNetInstrumentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.TestClasses
{
    public class TestResultDisplay : IResultDisplay
    {
        public IList<IProcessorResult> Results { get; } = new List<IProcessorResult>();

        public void Display(IProcessorResult result)
        {
            this.Results.Add(result);
        }
    }
}
