using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Core.Services;
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

namespace MSTR.PerformanceTesting.Core.Orchestrators
{
    public class ResultsFinalizerOrchestrator
    {
        public void Finalize(string previousReleaseJsonFilePath, string nextReleaseJsonFilePath)
        {
            // get previouse release results
            var previousReleaseJsonString = File.ReadAllText(previousReleaseJsonFilePath);
            var previousReleaseResults = JsonConvert.DeserializeObject<List<BenchmarkResult>>(previousReleaseJsonString);

            // get next release results
            var nextReleaseJsonString = File.ReadAllText(nextReleaseJsonFilePath);
            var nextReleaseResults = JsonConvert.DeserializeObject<List<BenchmarkResult>>(nextReleaseJsonString);

            // initalize recognizers list
            var recognizers = ConfigurationInitalizer.InitalizeRecognizersList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.RecognizersConfiguration));

            // get benchmark types
            var benchmarkTypes = Enum.GetValues(typeof(BenchmarkType)).Cast<BenchmarkType>();

            // create results finalizer
            var resultsFinalizer = new ResultsFinalizer();

            // get directories
            var mainDirectory = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory);
            var operationName = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.OperationName);

            foreach (var recognizer in recognizers)
            {
                Console.WriteLine($"Finalizing {recognizer.ToString()} results");

                foreach (var benchmarkType in benchmarkTypes)
                {
                    var results = resultsFinalizer.GetFinalResults(previousReleaseResults, nextReleaseResults, recognizer, benchmarkType, false);
                    var resultsPerTest = resultsFinalizer.GetFinalResults(previousReleaseResults, nextReleaseResults, recognizer, benchmarkType, true);
                    var path = Path.Combine(mainDirectory, Directories.resultsDirectory, operationName, recognizer.ToString());
                    CSVWriter.Write(results, path, $"{benchmarkType.ToString()}-{recognizer.ToString()}");
                    CSVWriter.Write(resultsPerTest, path, $"{benchmarkType.ToString()}-{recognizer.ToString()}-PerTest");
                }
            }
        }
    }
}
