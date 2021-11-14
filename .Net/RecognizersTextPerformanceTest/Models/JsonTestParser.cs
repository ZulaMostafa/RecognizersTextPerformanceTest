using Newtonsoft.Json;
using RecognizersTextPerformanceTest.Interfaces;
using System.Collections.Generic;

namespace RecognizersTextPerformanceTest.Models
{
    public class JsonTestParser : ITestParser
    {
        public List<T> Parse<T>(string rawData)
        {
            return JsonConvert.DeserializeObject<List<T>>(rawData);
        }
    }
}
