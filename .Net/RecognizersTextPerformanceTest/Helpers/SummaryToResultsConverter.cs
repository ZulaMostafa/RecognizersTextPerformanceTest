using BenchmarkDotNet.Reports;
using Common.enums;
using Common.Helpers;
using Common.ViewModels;
using System;

namespace RecognizersTextPerformanceTest.Helpers
{
    class SummaryToResultsConverter
    {
        public static BenchmarkResults Convert(Summary summary)
        {
            var performanceResults = new BenchmarkResults(1);

            foreach (var report in summary.Reports)
            {
                // get pararmeters
                var culture = report.BenchmarkCase.Parameters["culture"].ToString();
                var iteration = int.Parse(report.BenchmarkCase.Parameters["iteration"].ToString());
                var recognizer = report.BenchmarkCase.Parameters["recognizer"].ToString();
                var enumRecognizer = (Recognizers)Enum.Parse(typeof(Recognizers), recognizer);

                // get memory in MBs
                var memoryInMBs = report.Metrics["Allocated Memory"].Value;

                // get time in seconds
                var timeInSeconds = report.ResultStatistics.Mean;

                // get results
                var performanceMetrics = new BenchmarksMetrics()
                {
                    Memory = memoryInMBs,
                    Time = timeInSeconds
                };

                // add to results
                var list = performanceResults.GetDictionary(enumRecognizer.ToString(), iteration - 1);
                list.Add(culture, performanceMetrics);

            }

            return performanceResults;
        }
    }
}
