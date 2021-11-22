using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Orchestrator;
using RecognizersTextPerformanceTest.Services;
using System;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // load configs file
            var configsFile = ConfigsReader.LoadApplicationConfigs();

            // run tests
            var orchestrator = new ExecutingTestOrchestrator();
            var results = orchestrator.ExcecutePerformanceTests(configsFile.CulturesOption, configsFile.RecognizersOption, configsFile.IterationCount);

            // save results
            var resultsToCSV = new ResultsToCSV(configsFile.IterationCount);
            resultsToCSV.SaveAllResultsAsCSV(results, "test1");

            Console.ReadLine();
        }
    }
}
