using BenchmarkDotNet.Reports;
using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Helpers
{
    class SummaryToResultsConverter
    {
        public static PerformanceResults Convert(Summary summary)
        {
            var performanceResults = new PerformanceResults(3);

            foreach (var report in summary.Reports)
            {
                // get pararmeters
                var culture = report.BenchmarkCase.Parameters["culture"].ToString();
                var iteration = int.Parse(report.BenchmarkCase.Parameters["iteration"].ToString());
                var recognizer = report.BenchmarkCase.Parameters["recognizer"].ToString();
                var enumRecognizer = (Recognizers)Enum.Parse(typeof(Recognizers), recognizer);

                // get results
                var performanceMetrics = new PerformanceMetrics()
                {
                    Memory = report.GcStats.GetTotalAllocatedBytes(false) / 1024,
                    Time = Math.Round(report.ResultStatistics.Mean / 1000000, 3)
                };

                // add to results
                var list = performanceResults.GetDictionary(enumRecognizer, iteration - 1);
                list.Add(culture, performanceMetrics);

            }

            return performanceResults;
        }
    }
}
