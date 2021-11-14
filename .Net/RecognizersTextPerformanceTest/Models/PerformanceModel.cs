using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Diagnostics;

namespace RecognizersTextPerformanceTest.Models
{
    public class PerformanceModel : IPerformanceModel
    {
        private long _totalMemory;
        private long _totalTicks;

        public PerformanceModel()
        {
            _totalMemory = 0;
            _totalTicks = 0;
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
            _totalTicks = ticksAfterExecution - ticksBeforeExecution;
            _totalMemory = memoryAfterExecution - memoryBeforeExecution;
        }

        public string GetResults()
        {
            return string.Join(
                '\n',
                $"Total ticks: {_totalTicks}",
                $"Total Memory: {_totalMemory}");
        }
    }
}
