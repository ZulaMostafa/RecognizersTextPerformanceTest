﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using MSTR.PerformanceTesting.Core.Converters;
using MSTR.PerformanceTesting.Core.Helpers;
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
            /*var config = ManualConfig.Create(DefaultConfig.Instance)
                .WithSummaryStyle(SummaryStyle.Default
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second)
                .WithSizeUnit(BenchmarkDotNet.Columns.SizeUnit.MB));
            var summary = BenchmarkRunner.Run<dotNetRunner>(config);

            var results = BenchmarkResultConverter.From(summary);

            foreach (var x in results)
                Console.WriteLine(x.Culture + " " + x.Iteration + " " + x.Recognizer + " " + x.Type.ToString());

            ResultsLogger.LogResults(results);*/

            var config = ManualConfig.Create(DefaultConfig.Instance)
                 .WithSummaryStyle(SummaryStyle.Default
                 .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second)
                 .WithSizeUnit(BenchmarkDotNet.Columns.SizeUnit.MB));
            var summary = BenchmarkRunner.Run<PythonRunner>(config);

            var converter = new BenchmarkDotNetSummaryConverter();
            var results = converter.GetBenchmarkListResults(summary);

            foreach (var x in results)
                Console.WriteLine(x.Culture + " " + x.Iteration + " " + x.Recognizer + " " + x.Type.ToString());

            var logger = new ResultsLogger();
            logger.LogResults(results);

        }
    }
}