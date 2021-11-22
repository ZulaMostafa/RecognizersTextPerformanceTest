using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Diagnostics;

namespace RecognizersTextPerformanceTest.Models
{
    public class PerformanceModel : IPerformanceModel
    {
        private long _totalMemory;
        private double _totalSeconds;

        public PerformanceModel()
        {
            _totalMemory = 0;
            _totalSeconds = 0;
        }

        public void Measure(Action actionToBeDone)
        {
            // collect garbace
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            // measure memory before execution
            var memoryBeforeExecution = GC.GetTotalMemory(false);

            // measure ticks before execution
            var stopWatch = Stopwatch.StartNew();

            // execute action
            actionToBeDone();

            // calculate current ticks
            stopWatch.Stop();
            var ticks = stopWatch.Elapsed.TotalSeconds;

            // measure memory after execution
            var memoryAfterExecution = GC.GetTotalMemory(false);

            // add results
            _totalSeconds = ticks;
            _totalMemory = memoryAfterExecution - memoryBeforeExecution;
        }

        public long GetMemory()
        {
            return _totalMemory;
        }

        public double GetTime()
        {
            return _totalSeconds;
        }
    }
}
