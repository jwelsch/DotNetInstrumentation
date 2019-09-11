using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInstrumentation
{
    public class ProcessorRunner
    {
        private IEnumerable<IProcessor> processors;

        public ProcessorRunner(IEnumerable<IProcessor> processors)
        {
            this.processors = processors;
        }

        public async Task Start(HttpContext context)
        {
            if (this.processors != null)
            {
                foreach (var processor in this.processors)
                {
                    await processor.StartAsync(context);
                }
            }
        }

        public async Task Stop(HttpContext context, IResultDisplay resultDisplay)
        {
            if (this.processors != null)
            {
                foreach (var processor in this.processors)
                {
                    var result = await processor.StopAsync(context);

                    resultDisplay.Display(result);
                }
            }
        }
    }
}
