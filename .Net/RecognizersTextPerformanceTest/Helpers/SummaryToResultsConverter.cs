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
            var performanceResults = new BenchmarkResults(3);

            foreach (var report in summary.Reports)
            {
                // get pararmeters
                var culture = report.BenchmarkCase.Parameters["culture"].ToString();
                var iteration = int.Parse(report.BenchmarkCase.Parameters["iteration"].ToString());
                var recognizer = report.BenchmarkCase.Parameters["recognizer"].ToString();
                var enumRecognizer = (Recognizers)Enum.Parse(typeof(Recognizers), recognizer);

                // get memory in KBs
                var memoryInBytes = report.Metrics["Allocated Memory"].Value;
                var memoryInKBs = MemoryConverter.BytesToKBs(memoryInBytes);

                // get time in seconds
                var timeInNano = report.ResultStatistics.Mean;
                var timeInSeconds = TimeConverter.NanoToSeconds(timeInNano);


                // get results
                var performanceMetrics = new BenchmarksMetrics()
                {
                    Memory = memoryInKBs,
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
