using Newtonsoft.Json;
using RecognizersTextPerformanceTest.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RecognizersTextPerformanceTest.Interfaces;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.ViewModels;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // construct test parser
            var testParser = new JsonTestParser();

            // consturct tests loader
            var testsReader = new TestsReader<TestModel>(testParser);

            // create text reognizers client
            var languages = new List<string>()
            {
                "en-us"
            };
            var textRecognizerClient = new TextRecognizersClient(languages);

            // create measurement models
            var timeMeasurementModel = new TimeMeasurementModel();
            var memoryMeasurementModel = new MemoryMeasurementModel();

            // load tests
            var tests = testsReader.LoadTests("Specs");

            // run tests
            foreach (var test in tests)
            {
                var input = test.Input;
                timeMeasurementModel.Measure(() => textRecognizerClient.RunTest(input));
                memoryMeasurementModel.Measure(() => textRecognizerClient.RunTest(input));
            }

            // get time result
            var elapsedMilliseconds = (long)timeMeasurementModel.getResult();
            var elapsedTime = TimeConverter.ConvertMillisecondsToTimeFormat(elapsedMilliseconds);

            // get memory result
            var kbMemory = (long)memoryMeasurementModel.getResult();

            // print results
            Console.WriteLine($"Total time: {elapsedTime}");
            Console.WriteLine($"Total memory: {kbMemory}");
        }
    }
}
