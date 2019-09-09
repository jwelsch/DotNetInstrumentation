using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetInstrumentation
{
    public interface IBodyStatistics
    {
        long Minimum
        {
            get;
        }

        long Maximum
        {
            get;
        }

        double Average
        {
            get;
        }

        long Total
        {
            get;
        }

        int Count
        {
            get;
        }
    }
}
