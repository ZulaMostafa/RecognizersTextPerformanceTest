using BenchmarkDotNet.Running;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Orchestrator;
using RecognizersTextPerformanceTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var resultsToCSV = new ResultsToCSV(3);
            resultsToCSV.SaveAllResultsAsCSV(results, "library-test");

            /*  // load tests
              

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
                  configsFile.IterationCount);

              // save results
              var resultsToCSV = new ResultsToCSV(configsFile.IterationCount);
              resultsToCSV.SaveAllResultsAsCSV(results, configsFile.OperationName); */

        }
    }
}
