using ScriptRunner.Helpers;
using ScriptRunner.Orchestrators;
using ScriptRunner.Services;
using System;

namespace ScriptRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            // load configs file
            var configFile = ConfigsReader.LoadApplicationConfigs();

            // get results
            var results = ExecutingTestsOrchestrator.ExcecutePerformanceTests(configFile);

            // save results
            var resultsToCSV = new ResultsToCSV(configFile.IterationCount);
            resultsToCSV.SaveAllResultsAsCSV(results, configFile.OperationName);
        }
    }
}
