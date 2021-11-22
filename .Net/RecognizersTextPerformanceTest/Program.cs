using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Orchestrator;
using RecognizersTextPerformanceTest.Services;
using System;
using System.Collections.Generic;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // load configs file
            var configsFile = ConfigsReader.LoadApplicationConfigs();

            // load tests
            var culturesTests = new Dictionary<string, List<string>>();
            foreach (var culture in Constants.cultures)
            {
                var tests = TestsReader.ReadTests(configsFile.RootPath, culture);
                culturesTests.Add(culture, tests);
            }

            // create recognizers clients
            var recognizersClients = new Dictionary<string, TextRecognizersClient>();
            foreach (var culture in Constants.cultures)
            {
                foreach (var recognizer in Constants.recognizers)
                {
                    var key = $"{recognizer}-{culture}";
                    var recognizerClient = new TextRecognizersClient(culture, recognizer);
                    recognizersClients.Add(key, recognizerClient);
                }
            }

            // create performance model
            var performanceModel = new PerformanceModel();

            // run tests
            var orchestrator = new ExecutingTestOrchestrator();
            var results = orchestrator.ExcecutePerformanceTests(
                configsFile.CulturesOption,
                configsFile.RecognizersOption,
                configsFile.IterationCount,
                performanceModel,
                culturesTests,
                recognizersClients);

            // save results
            var resultsToCSV = new ResultsToCSV(configsFile.IterationCount);
            resultsToCSV.SaveAllResultsAsCSV(results, "test1");

        }
    }
}
