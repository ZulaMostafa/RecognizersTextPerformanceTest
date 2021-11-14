using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Models
{
    public class TimeMeasurementModel: IMeasurementModel
    {
        private long ElapsedMilliseconds;
        public TimeMeasurementModel()
        {
            ElapsedMilliseconds = 0;
        }

        public void Measure(Action actionToBeDone)
        {
            // start stopwatch
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            // execute action
            actionToBeDone();

            // stop stopwatch
            stopWatch.Stop();

            ElapsedMilliseconds += stopWatch.ElapsedMilliseconds;
        }

        public object getResult()
        {
            return ElapsedMilliseconds;
        }
    }
}
