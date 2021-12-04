using BenchmarkDotNet.Running;
using Common.Helpers;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Services;
using System.Collections.Generic;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        public static Dictionary<string, List<string>> culturesTests = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            // load configs file
            //var configsFile = ConfigsReader.LoadApplicationConfigs();

            var summary = BenchmarkRunner.Run<TestRunner>();

            var results = SummaryToResultsConverter.Convert(summary);

            var resultsToCSV = new ResultsToCSV(1);
            resultsToCSV.SaveAllResultsAsCSV(results, "japanese");
        }
    }
}
