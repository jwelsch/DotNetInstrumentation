using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInstrumentation
{
    public interface IProcessor
    {
        Task StartAsync(HttpContext context);

        Task<IProcessorResult> StopAsync(HttpContext context);
    }
}
