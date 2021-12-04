using BenchmarkDotNet.Attributes;
using Common.enums;
using Common.Helpers;
using System;
using System.IO;

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
            var Directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\..\\..\\..\\..\\", "alltests"));
            var tests = TestsReader.ReadTests(Directory, culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
