using BenchmarkDotNet.Attributes;
using Common;
using Common.enums;
using Common.Helpers;
using Common.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace RecognizersTextPerformanceTest.Services
{
    [MemoryDiagnoser]
    public class TestRunner
    {   
        public IEnumerable<string> Cultures 
            => ConfigurationInitalizer.InitalizeCulturesList(EnvironmentVariables.GetCulturesConfiguration());

        public IEnumerable<Recognizers> Recognizers
            => ConfigurationInitalizer.InitalizeRecognizersList(EnvironmentVariables.GetRecognizersConfiguration());

        [ParamsSource(nameof(Cultures))]
        public string culture { get; set; }

        [ParamsSource(nameof(Recognizers))]
        public Recognizers recognizer { get; set; }

        [Params(1)]
        public int iteration;

        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            var client = new TextRecognizersClient(culture, recognizer);
            var Directory = Path.Combine(EnvironmentVariables.GetMainDirectory(), Constants.TestsDirectory);
            var tests = TestsReader.ReadTests(Directory, culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
