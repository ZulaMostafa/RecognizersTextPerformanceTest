using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RecognizersTextPerformanceTest.Orchestrator
{
    public class ExecutingTestOrchestrator
    {
        public PerformanceResults ExcecutePerformanceTests(string culturesOption, string recognizersOption, int iterationCount)
        {
            // Initalize recognizers and cultures list
            var recognizers = InitalizeRecognizersList(recognizersOption);
            var cultures = InitalizeCulturesList(culturesOption);

            // Initalize results carrier
            PerformanceResults performanceResults = new PerformanceResults(iterationCount);

            foreach (var recognizer in recognizers)
            {
                for (int iteration = 1; iteration <= iterationCount; iteration++)
                {
                    foreach (var culture in cultures)
                    {
                        Thread.Sleep(800);

                        var testResults = ExecuteTest(culture, recognizer);

                        // get specific result dictionary for the current recognizer and iteration
                        var dictionary = performanceResults.GetDictionary(recognizer, iteration - 1);

                        // add result to specific culture
                        dictionary.Add(culture, testResults);

                        // log
                        Console.WriteLine($"{recognizer}-{iteration}-{culture} Done");
                    }
                }
            }

            return performanceResults;
        }

        private List<Recognizers> InitalizeRecognizersList(string recognizersOptions)
        {
            if (recognizersOptions.ToLower() == "all")
                return Constants.recognizers;

            var recognizers = new List<Recognizers>() { (Recognizers)Enum.Parse(typeof(Recognizers), recognizersOptions) };
            return recognizers;
        }

        private List<string> InitalizeCulturesList(string culturesOptions)
        {
            if (culturesOptions.ToLower() == "all")
                return Constants.cultures;

            var cultures = new List<string>() { culturesOptions };
            return cultures;
        }

        private PerformanceMetrics ExecuteTest(string culture, Recognizers recognizer)
        {
            // read test file
            var testInputs = TestsReader.ReadTests("tests", culture);

            // initalize text recognizers client
            var textRecognizersClient = new TextRecognizersClient(culture, recognizer);

            // initalize performance model
            var performanceModel = new PerformanceModel();

            // execute test
            performanceModel.Measure(() =>
            {
                foreach (var testInput in testInputs)
                    textRecognizersClient.RunTest(testInput);
            });

            // get results
            var performanceResults = new PerformanceMetrics()
            {
                Memory = performanceModel.GetMemory(),
                Time = performanceModel.GetTime()
            };

            return performanceResults;
        }
    }
}
