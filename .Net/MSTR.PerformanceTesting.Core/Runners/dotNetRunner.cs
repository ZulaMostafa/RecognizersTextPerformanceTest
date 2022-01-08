using BenchmarkDotNet.Attributes;
using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Core.Services;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Runners
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 10)]
    public class dotNetRunner
    {
        public IEnumerable<string> Cultures
            => ConfigurationInitalizer.InitalizeCulturesList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.CulturesConfiguration));

        public IEnumerable<Recognizers> Recognizers
            => ConfigurationInitalizer.InitalizeRecognizersList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.RecognizersConfiguration));

        public IEnumerable<int> Iterations
            => Enumerable.Range(1, int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.IterationsConfiguration)));

        [ParamsSource(nameof(Cultures))]
        public string culture { get; set; }

        [ParamsSource(nameof(Recognizers))]
        public Recognizers recognizer { get; set; }

        [ParamsSource(nameof(Iterations))]
        public int iteration;

        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            var client = new TextRecognizersClient(culture, recognizer);
            var Directory = Path.Combine(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory), Directories.TestsDirectory);
            var tests = TestsReader.ReadTests(Directory, culture);

            foreach (var test in tests)
                client.RunTest(test);
        }
    }
}
