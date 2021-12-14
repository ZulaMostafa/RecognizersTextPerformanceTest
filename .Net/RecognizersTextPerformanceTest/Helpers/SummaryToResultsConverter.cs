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

            Console.WriteLine("SSSSSSSSSSSS: " + summary.Style.TimeUnit);

            foreach (var report in summary.Reports)
            {
                // get pararmeters
                var culture = report.BenchmarkCase.Parameters["culture"].ToString();
                var iteration = int.Parse(report.BenchmarkCase.Parameters["iteration"].ToString());
                var recognizer = report.BenchmarkCase.Parameters["recognizer"].ToString();
                var enumRecognizer = (Recognizers)Enum.Parse(typeof(Recognizers), recognizer);

                // get memory in MBs
                var memoryInBytes = report.Metrics["Allocated Memory"].Value;
                var memoryInMBs = MemoryConverter.BytesToMBs(memoryInBytes);

                // get time in seconds
                var timeInNano = report.ResultStatistics.Mean;
                var timeInSeconds = TimeConverter.NanoToSeconds(timeInNano);

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
