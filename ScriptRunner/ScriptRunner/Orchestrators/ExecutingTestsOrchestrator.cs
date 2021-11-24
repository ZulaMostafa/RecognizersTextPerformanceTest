using Common;
using Common.enums;
using Common.Helpers;
using Common.ViewModels;
using ScriptRunner.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptRunner.Orchestrators
{
    public class ExecutingTestsOrchestrator
    {
        public static BenchmarkResults ExcecutePerformanceTests(ConfigModel configs)
        {
            // Initalize recognizers and cultures list
            var recognizers = InitalizeRecognizersList(configs.RecognizersOption);
            var cultures = InitalizeCulturesList(configs.CulturesOption);

            // Initalize results carrier
            var performanceResults = new BenchmarkResults(configs.IterationCount);

            foreach (var recognizer in recognizers)
            {
                for (int iteration = 1; iteration <= configs.IterationCount; iteration++)
                {
                    foreach (var culture in cultures)
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

                        var testResults = new BenchmarksMetrics()
                        {
                            Memory = memoryInKBs,
                            Time = timeInSeconds
                        };

                        // get specific result dictionary for the current recognizer and iteration
                        var dictionary = performanceResults.GetDictionary(recognizer.ToString(), iteration - 1);

                        // add result to specific culture
                        dictionary.Add(culture, testResults);

                        // log
                        Console.WriteLine($"{recognizer}-{iteration}-{culture} Done");
                    }
                }
            }

            return performanceResults;
        }

        public static List<Recognizers> InitalizeRecognizersList(string recognizersOptions)
        {
            if (recognizersOptions.ToLower() == "all")
                return Constants.recognizers;

            var recognizers = new List<Recognizers>() { (Recognizers)Enum.Parse(typeof(Recognizers), recognizersOptions) };
            return recognizers;
        }

        public static List<string> InitalizeCulturesList(string culturesOptions)
        {
            if (culturesOptions.ToLower() == "all")
                return Constants.cultures;

            var cultures = new List<string>() { culturesOptions };
            return cultures;
        }
    }
}
