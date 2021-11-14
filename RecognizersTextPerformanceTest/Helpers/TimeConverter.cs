using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Helpers
{
    public static class TimeConverter
    {
        public static string ConvertMillisecondsToTimeFormat(long milliseconds)
        {
            var timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            return timeSpan.ToString(@"hh\:mm\:ss");
        }
    }
}
