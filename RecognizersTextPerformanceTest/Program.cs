using Microsoft.Recognizers.Text;
using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.ViewModels;
using System;

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
            var culture = Culture.English;
            var recognizer = Recognizers.Choice;
            var textRecognizerClient = new TextRecognizersClient(culture, recognizer);

            // create performance model
            var performanceModel = new PerformanceModel();

            // load tests
            var tests = testsReader.LoadTests($"tests\\{nameof(Culture.English)}.json");

            // run all tests
            performanceModel.Measure(() =>
            {
                foreach (var test in tests)
                    textRecognizerClient.RunTest(test.Input);
            });

            // print results
            Console.WriteLine(performanceModel.GetResults());
        }
    }
}
