using BenchmarkDotNet.Attributes;
using Common.enums;
using Common.Helpers;

namespace RecognizersTextPerformanceTest.Services
{
    [MemoryDiagnoser]
    public class TestRunner
    {
        [Params(1, 2, 3, 4, 5)]
        public int iteration { get; set; }

        [Params("Arabic",
            "Bulgarian",
            "Chinese",
            "Dutch",
            "English",
            "French",
            "German",
            "Hindi",
            "Italian",
            "Japanese",
            "Korean",
            "Portuguese",
            "Spanish",
            "Swedish",
            "Turkish")]

        public string culture { get; set; }

        [Params(Recognizers.DateTime)]

        public Recognizers recognizer { get; set; }


        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            var client = new TextRecognizersClient(culture, Recognizers.Choice);
            var tests = TestsReader.ReadTests("C:\\tests", culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
