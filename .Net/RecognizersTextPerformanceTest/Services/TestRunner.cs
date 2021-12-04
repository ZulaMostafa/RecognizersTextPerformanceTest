using BenchmarkDotNet.Attributes;
using Common.enums;
using Common.Helpers;

namespace RecognizersTextPerformanceTest.Services
{
    [MemoryDiagnoser]
    public class TestRunner
    {
        [Params(1)]
        public int iteration { get; set; }

        [Params("English")]

        public string culture { get; set; }

        [Params(Recognizers.Choice)]

        public Recognizers recognizer { get; set; }


        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            var client = new TextRecognizersClient(culture, recognizer);
            var tests = TestsReader.ReadTests("..\\tests", culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
