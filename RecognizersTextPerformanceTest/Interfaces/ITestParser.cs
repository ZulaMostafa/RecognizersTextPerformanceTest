using System.Collections.Generic;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface ITestParser
    {
        public List<T> Parse<T>(string rawData);
    }
}
