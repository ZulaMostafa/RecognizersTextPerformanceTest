using Newtonsoft.Json;
using RecognizersTextPerformanceTest.Interfaces;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Models
{
    public class JsonTestParser: ITestParser
    {
        public List<T> Parse<T>(string rawData)
        {
            return JsonConvert.DeserializeObject<List<T>>(rawData);
        }
    }
}
