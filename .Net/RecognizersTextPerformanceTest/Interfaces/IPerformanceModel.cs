using System;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface IPerformanceModel
    {
        public void Measure(Action actionToBeDone);
    }
}
