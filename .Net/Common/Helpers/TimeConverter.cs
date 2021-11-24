using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class TimeConverter
    {
        public static double NanoToSeconds(double nano)
        {
            return nano / 1000000000;
        }
    }
}
