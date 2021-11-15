using Microsoft.Recognizers.Text;
using RecognizersTextPerformanceTest.enums;
using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.Models.Logger;
using RecognizersTextPerformanceTest.Services;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            // load test files
            var testFiles = configsFile.cultures.Select(culture =>
            {
                return File.ReadAllText(Path.Combine(configsFile.RootPath, $"{culture}.json"));
            }).ToList();

            // cast test files
            var tests = testFiles.SelectMany(file =>
            {
                return JsonHandler.DeserializeObject<List<TestModel>>(file);
            });
            
            performanceModel.Measure(() =>
            {
                foreach (var test in tests)
                    textRecognizerClient.RunTest(test.Input);
            });

            // init logging service
            var loggingService = new LoggingService();

            // register loggers [TODO: add types to config giles]
            loggingService.RegisterLogger(new ConsoleLogger());
            loggingService.RegisterLogger(new FileLogger());

            loggingService.Log(configsFile.OperationName, performanceModel.GetResults());
        }
    }
}
