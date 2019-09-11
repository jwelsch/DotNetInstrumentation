using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public class ProcessorRunner
    {
        public IEnumerable<IProcessor> processors;

        public ProcessorRunner(IEnumerable<IProcessor> processors)
        {
            this.processors = processors;
        }

        public void Start(HttpContext context)
        {
            foreach (var processor in this.processors)
            {
                processor.Start(context);
            }
        }

        public void Stop(HttpContext context, IResultDisplay resultDisplay)
        {
            foreach (var processor in this.processors)
            {
                var result = processor.Stop(context);

                resultDisplay.Display(result);
            }
        }
    }
}
