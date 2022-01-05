using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services.Converters
{
    public static class UnitsConverter
    {
        public static double GetSecondsFromNano(double nanoseconds)
        {
            return nanoseconds / 1000000000;
        }

        public static double GetMBsFromBytes(double Bytes)
        {
            return Bytes / (1024 * 1024);
        }
    }
}
