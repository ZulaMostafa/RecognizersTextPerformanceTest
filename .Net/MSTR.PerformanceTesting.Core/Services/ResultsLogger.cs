using MSTR.PerformanceTesting.Core.Converters;
using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services
{
    public static class ResultsLogger
    {
        public static void LogResults(List<BenchmarkResult> results)
        {
            // initalize recognizers list
            var recognizers = ConfigurationInitalizer.InitalizeRecognizersList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.RecognizersConfiguration));

            // get benchmark types
            var benchmarkTypes = Enum.GetValues(typeof(BenchmarkType)).Cast<BenchmarkType>();

            foreach (var benchmarkType in benchmarkTypes)
            {
                foreach (var recognizer in recognizers)
                {
                    // construct path
                    var mainDirectory = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory);
                    var operationName = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.OperationName);
                    var path = Path.Combine(mainDirectory, Directories.resultsDirectory, operationName, recognizer.ToString());

                    // get table results
                    var tableResults = SingleReleaseTableConverter.From(results, recognizer, benchmarkType);

                    // write results into file
                    CSVWriter.Write(tableResults, path, $"{recognizer.ToString()}-{benchmarkType.ToString()}");
                }
            }
        }
    }
}
