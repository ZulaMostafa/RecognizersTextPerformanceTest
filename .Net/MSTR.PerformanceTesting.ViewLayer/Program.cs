using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Core.Orchestrators;
using MSTR.PerformanceTesting.Core.Runners;
using MSTR.PerformanceTesting.Core.Services;
using MSTR.PerformanceTesting.Core.Services.Converters;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

namespace MSTR.ViewLayer
{
    class Program
    {
        public static void Main(string[] args)
        {
            string type = args[0];
            
            switch (type)
            {
                case "test":
                    var config = ManualConfig.Create(DefaultConfig.Instance)
                 .WithSummaryStyle(SummaryStyle.Default
                 .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second)
                 .WithSizeUnit(BenchmarkDotNet.Columns.SizeUnit.MB));

                    var summary = BenchmarkRunner.Run<dotNetRunner>(config);

                    var converter = new BenchmarkDotNetSummaryConverter();
                    var results = converter.GetBenchmarkListResults(summary);

                    var logger = new ResultsLogger();
                    logger.LogResults(results);
                    break;
                case "finalize":
                    var finalizingOrchestrator = new ResultsFinalizerOrchestrator();

                    var mainDirectory = Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory);

                    var previousReleaseFolder = args[1];
                    var nextReleaseFolder = args[2];

                    string previousReleaseJsonFilePath = Path.Combine(mainDirectory, previousReleaseFolder, "results.json");
                    string nextReleaseJsonFilePath = Path.Combine(mainDirectory, nextReleaseFolder, "results.json");
                    
                    finalizingOrchestrator.Finalize(previousReleaseJsonFilePath, nextReleaseJsonFilePath);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }
    }
}