using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Common;
using Common.Helpers;
using Common.Services;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ManualConfig()
                .WithSummaryStyle(SummaryStyle.Default
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second)
                .WithSizeUnit(BenchmarkDotNet.Columns.SizeUnit.MB));
            var summary = BenchmarkRunner.Run<TestRunner>(config);

            var results = SummaryToResultsConverter.Convert(summary);

            var resultsToCSV = new ResultsToCSV(1);
            var path = Path.Combine(EnvironmentVariables.GetMainDirectory(), Constants.ResultsDirectory, EnvironmentVariables.GetOperationName());
            resultsToCSV.SaveAllResultsAsCSV(results, path);
        }
    }
}
