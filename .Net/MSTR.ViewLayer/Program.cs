using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using MSTR.PerformanceTesting.Core.Converters;
using MSTR.PerformanceTesting.Core.Runners;
using MSTR.PerformanceTesting.Core.Services;
using MSTR.PerformanceTesting.Definitions.Enums;
using System;

namespace MSTR.ViewLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .WithSummaryStyle(SummaryStyle.Default
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second)
                .WithSizeUnit(BenchmarkDotNet.Columns.SizeUnit.MB));
            var summary = BenchmarkRunner.Run<dotNetRunner>(config);

            var results = BenchmarkResultConverter.From(summary);

            foreach (var x in results)
                Console.WriteLine(x.Culture + " " + x.Iteration + " " + x.Recognizer + " " + x.Type.ToString());

            ResultsLogger.LogResults(results);
        }
    }
}
