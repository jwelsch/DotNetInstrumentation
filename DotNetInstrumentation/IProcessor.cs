using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public interface IProcessor
    {
        void Start(HttpContext context);

        IProcessorResult Stop(HttpContext context);
    }
}
