using BenchmarkDotNet.Running;
using Common;
using Common.Helpers;
using Common.Services;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Services;
using System.Collections.Generic;
using System.IO;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();

            var results = SummaryToResultsConverter.Convert(summary);

            var resultsToCSV = new ResultsToCSV(1);
            var path = Path.Combine(EnvironmentVariables.GetMainDirectory(), Constants.ResultsDirectory, EnvironmentVariables.GetOperationName());
            resultsToCSV.SaveAllResultsAsCSV(results, path);
        }
    }
}
