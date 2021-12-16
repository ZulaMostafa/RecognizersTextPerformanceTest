using BenchmarkDotNet.Reports;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Converters
{
    public static class BenchmarkResultConverter
    {
        public static List<BenchmarkResult> From(Summary summary)
        {
            var benchmarkResults = new List<BenchmarkResult>();

            foreach (var report in summary.Reports)
            {
                var currentCaseResults = ExtractResultsFromReport(report);
                benchmarkResults.AddRange(currentCaseResults);
            }

            return benchmarkResults;
        }

        private static List<BenchmarkResult> ExtractResultsFromReport(BenchmarkReport report)
        {
            var reportBenchmarkResults = new List<BenchmarkResult>();

            // get result base
            var benchmarkResultBase = CreateBenchmarkResultBase(report);

            // add memory resullt
            var memoryResult = GetMemoryResult(report, benchmarkResultBase);
            reportBenchmarkResults.Add(memoryResult);

            // add time result
            var timeResult = GetTimeResult(report, benchmarkResultBase);
            reportBenchmarkResults.Add(timeResult);

            return reportBenchmarkResults;
        }

        private static BenchmarkResult CreateBenchmarkResultBase(BenchmarkReport report)
        {
            // get benchmark case parameters
            var culture = report.BenchmarkCase.Parameters["culture"].ToString();
            var iteration = int.Parse(report.BenchmarkCase.Parameters["iteration"].ToString());
            var recognizer = report.BenchmarkCase.Parameters["recognizer"].ToString();

            // get enum recognizer
            var enumRecognizer = (Recognizers)Enum.Parse(typeof(Recognizers), recognizer);

            return new BenchmarkResult()
            {
                Culture = culture,
                Iteration = iteration,
                Recognizer = enumRecognizer,
            };
        }

        private static BenchmarkResult GetMemoryResult(BenchmarkReport report, BenchmarkResult benchmarkResult)
        {
            benchmarkResult.Type = BenchmarkType.Memory;
            benchmarkResult.Value = report.Metrics["Allocated Memory"].Value;
            return benchmarkResult;
        }

        private static BenchmarkResult GetTimeResult(BenchmarkReport report, BenchmarkResult benchmarkResult)
        {
            benchmarkResult.Type = BenchmarkType.Time;
            benchmarkResult.Value = report.ResultStatistics.Mean;
            return benchmarkResult;
        }
    } 
}
