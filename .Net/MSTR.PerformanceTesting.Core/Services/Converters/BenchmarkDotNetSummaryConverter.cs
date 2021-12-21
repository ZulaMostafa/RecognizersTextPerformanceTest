using BenchmarkDotNet.Reports;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services.Converters
{
    public class BenchmarkDotNetSummaryConverter
    {

        public List<BenchmarkResult> GetBenchmarkListResults(Summary summary)
        {
            var benchmarkResults = new List<BenchmarkResult>();
            var types = Enum.GetValues(typeof(BenchmarkType)).Cast<BenchmarkType>();
            foreach (var report in summary.Reports)
            {
                foreach (var type in types)
                {
                    var result = GetBenchmarkResult(report, type);
                    benchmarkResults.Add(result);
                }
            }
            return benchmarkResults;
        }

        private BenchmarkResult GetBenchmarkResult(BenchmarkReport report, BenchmarkType type)
        {
            var benchmarkResult = CreateBenchmarkResultBase(report);
            benchmarkResult.Type = type;
            benchmarkResult.Value = GetBenchmarkValue(report, type);
            return benchmarkResult;
        }

        private double GetBenchmarkValue(BenchmarkReport report, BenchmarkType type)
        {
            double value = 0;
            switch (type)
            {
                case BenchmarkType.Memory:
                    value = GetMemory(report);
                    break;
                case BenchmarkType.Time:
                    value = GetTime(report);
                    break;
            }
            value = Math.Round(value, 3);
            return value;
        }

        private double GetMemory(BenchmarkReport report)
        {
            var memoryInBytes = report.Metrics["Allocated Memory"].Value;
            var memoryInMBs = UnitsConverter.GetMBsFromBytes(memoryInBytes);
            return memoryInMBs;
        }

        private double GetTime(BenchmarkReport report)
        {
            var timeInNanoSecodns = report.ResultStatistics.Mean;
            var timeInSeconds = UnitsConverter.GetSecondsFromNano(timeInNanoSecodns);
            return timeInSeconds;
        }
        private BenchmarkResult CreateBenchmarkResultBase(BenchmarkReport report)
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
                Recognizer = enumRecognizer
            };
        }
    }
}
