using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class MemoryConverter
    {
        public static double BytesToKBs(double bytes)
        {
            return bytes / 1024;
        }
    }
}
