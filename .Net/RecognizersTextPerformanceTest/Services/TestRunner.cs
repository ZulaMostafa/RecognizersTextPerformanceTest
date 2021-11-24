using BenchmarkDotNet.Attributes;
using RecognizersTextPerformanceTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Services
{
    [MemoryDiagnoser]
    public class TestRunner
    {
        [Params(1, 2)]
        public int iteration { get; set; }

        [Params("Arabic",
            "Bulgarian")]
        
        public string culture { get; set; }

        [Params("Choice")]

        public string recognizer { get; set; }


        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            var client = new TextRecognizersClient(culture, enums.Recognizers.Choice);
            var tests = TestsReader.ReadTests("C:\\tests", culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
