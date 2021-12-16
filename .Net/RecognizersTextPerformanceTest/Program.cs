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
            config.AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
            config.AddExporter(DefaultConfig.Instance.GetExporters().ToArray());
            config.AddAnalyser(DefaultConfig.Instance.GetAnalysers().ToArray());
            config.AddJob(DefaultConfig.Instance.GetJobs().ToArray());
            config.AddValidator(DefaultConfig.Instance.GetValidators().ToArray());
            config.AddLogger(DefaultConfig.Instance.GetLoggers().ToArray());
            config.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default
            var summary = BenchmarkRunner.Run<TestRunner>(config);

            var results = SummaryToResultsConverter.Convert(summary);

            var resultsToCSV = new ResultsToCSV(1);
            var path = Path.Combine(EnvironmentVariables.GetMainDirectory(), Constants.ResultsDirectory, EnvironmentVariables.GetOperationName());
            resultsToCSV.SaveAllResultsAsCSV(results, path);
        }
    }
}
