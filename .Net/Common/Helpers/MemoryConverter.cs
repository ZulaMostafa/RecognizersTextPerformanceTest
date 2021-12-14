using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class MemoryConverter
    {
        public static double BytesToMBs(double bytes)
        {
            return bytes / (1024 * 1024);
        }
    }
}
