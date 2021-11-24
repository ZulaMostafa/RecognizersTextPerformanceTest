using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RecognizersTextPerformanceTest.Orchestrator
{
    public class ExecutingTestOrchestrator
    {
        public PerformanceResults ExcecutePerformanceTests(string culturesOption,
            string recognizersOption,
            int iterationCount)
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
                    var randomized = cultures.ToList().OrderBy(x => Guid.NewGuid()).ToList();
                    foreach (var culture in randomized)
                    {
                        // get test set
                        var tests = TestsReader.ReadTests("tests", culture);

                        // get specific recognizer
                        var key = $"{recognizer}-{culture}";
                        var recognizerClient = new TextRecognizersClient(culture, recognizer);

                        // create performance model
                        var performanceModel = new PerformanceModel();

                        // execute tests
                        /*performanceModel.Measure(() =>
                        {
                            foreach (var test in tests)
                                recognizerClient.RunTest(test);
                        });*/


                        // get results
                        var testResults = new PerformanceMetrics()
                        {
                            Memory = performanceModel.GetMemory(),
                            Time = performanceModel.GetTime()
                        };

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

        [Benchmark(Baseline = true)]
        public void test(TextRecognizersClient c, List<string> tests)
        {
            foreach (var test in tests)
                c.RunTest(test);
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
    }
}
