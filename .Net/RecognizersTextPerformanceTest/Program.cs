using Microsoft.Recognizers.Text;
using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace RecognizersTextPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // load configs file
            var configsFile = ConfigsReader.LoadApplicationConfigs();

            // create text reognizers client
            var textRecognizerClient = new TextRecognizersClient(configsFile.cultures, configsFile.recognizers);

            // create performance model
            var performanceModel = new PerformanceModel();

            // load tests
            var tests = new List<TestModel>();
            foreach (var culture in configsFile.cultures)
            {
                var path = Path.Combine(configsFile.RootPath, $"{culture}.json");
                var text = File.ReadAllText(path);
                var cultureTests = JsonHandler.DeserializeObject<List<TestModel>>(text);
                tests.AddRange(cultureTests);
            }
            
            performanceModel.Measure(() =>
            {
                foreach (var test in tests)
                    textRecognizerClient.RunTest(test.Input);
            });
        }
    }
}
