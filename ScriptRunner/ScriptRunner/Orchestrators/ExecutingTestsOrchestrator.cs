using Common;
using Common.enums;
using Common.Helpers;
using Common.ViewModels;
using ScriptRunner.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptRunner.Orchestrators
{
    public class ExecutingTestsOrchestrator
    {
        public static BenchmarkResults ExcecutePerformanceTests(ConfigModel configs)
        {
            int innterIterationCount = 51;

            // Initalize recognizers and cultures list
            var recognizers = ConfigurationInitalizer.InitalizeRecognizersList(configs.RecognizersOption);
            var cultures = ConfigurationInitalizer.InitalizeCulturesList(configs.CulturesOption);

            // Initalize results carrier
            var performanceResults = new BenchmarkResults(configs.IterationCount);

            foreach (var recognizer in recognizers)
            {
                for (int iteration = 1; iteration <= configs.IterationCount; iteration++)
                {
                    foreach (var culture in cultures)
                    {
                        var memoryList = new List<double>();
                        var timeList = new List<double>();
                        for (int innerIteration = 1; innerIteration <= innterIterationCount; innerIteration++)
                        {
                            // run test
                            var testPath = Path.Combine(configs.TestsRootPath, $"{culture}.json");
                            var args = String.Join(' ', culture, recognizer, testPath);
                            var output = ProcessExecuter.GetPythonOutput(configs.PythonPath, configs.ScriptPath, args);

                            // get results
                            var splittedOutput = output.Split(' ');

                            var memoryInBytes = double.Parse(splittedOutput[0]);
                            var memoryInKBs = MemoryConverter.BytesToKBs(memoryInBytes);

                            var timeInSeconds = double.Parse(splittedOutput[1]);

                            memoryList.Add(memoryInKBs);
                            timeList.Add(timeInSeconds);

                            // log
                            Console.WriteLine($"{recognizer}-{iteration}-{culture}-{innerIteration} Done");
                        }

                        Console.WriteLine(memoryList.Count);

                        var finalMemoryList = OutliersRemover.Remove(memoryList);
                        var finalTimeList = OutliersRemover.Remove(timeList);

                        var testResults = new BenchmarksMetrics()
                        {
                            Memory = finalMemoryList.Average(),
                            Time = finalTimeList.Average()
                        };

                        // get specific result dictionary for the current recognizer and iteration
                        var dictionary = performanceResults.GetDictionary(recognizer.ToString(), iteration - 1);

                        // add result to specific culture
                        dictionary.Add(culture, testResults);
                    }
                }
            }

            return performanceResults;
        }
    }
}
