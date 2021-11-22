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
            // measure memory before execution
            var memoryBeforeExecution = Process.GetCurrentProcess().WorkingSet64;

            // measure ticks before execution
            var ticksBeforeExecution = DateTime.Now.Ticks;

            // execute action
            actionToBeDone();

            // measure ticks after execution
            var ticksAfterExecution = DateTime.Now.Ticks;

            // measure memory after execution
            var memoryAfterExecution = Process.GetCurrentProcess().WorkingSet64;

            // add results
            _totalSeconds = TimeSpan.FromTicks(ticksAfterExecution - ticksBeforeExecution).TotalSeconds;
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
