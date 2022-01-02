using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Core.Services.Converters;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services
{
    public class ResultsLogger
    {
        public void LogResults(List<BenchmarkResult> results)
        {
            LogCSV(results);
            LogJson(results);
        }

        public void LogCSV(List<BenchmarkResult> results)
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
                    var benchmarkResultsListConverter = new BenchmarkResultListConverter();
                    var tableResults = benchmarkResultsListConverter.GetTable(results, recognizer, benchmarkType);

                    // write results into file
                    CSVWriter.Write(tableResults, path, $"{recognizer.ToString()}-{benchmarkType.ToString()}");
                }
            }
        }

        public void LogJson(List<BenchmarkResult> results)
        {
            string jsonString = JsonConvert.SerializeObject(results);
            var mainDirectory = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory);
            var operationName = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.OperationName);
            string path = Path.Combine(mainDirectory, Directories.resultsDirectory, operationName, "results.json");
            File.WriteAllText(path, jsonString);
        }
    }
}
