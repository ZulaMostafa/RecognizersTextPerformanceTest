using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Converters
{
    public static class SingleReleaseTableConverter
    {
        public static List<List<string>> From(List<BenchmarkResult> results, Recognizers recognizer, BenchmarkType benchmarkType)
        {
            var result = new List<List<string>>();

            // get configurations
            var testedCultures = ConfigurationInitalizer.InitalizeCulturesList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.CulturesConfiguration));
            var iterationsCount = int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.IterationsConfiguration));

            // initialize columns
            var columns = InitilaizeColumns(testedCultures);
            result.Add(columns);

            // get recognizer results
            var recognizerResults = results.Where(r => r.Recognizer == recognizer).ToList();

            // iterate over results
            for (int i = 1; i <= iterationsCount; i++)
            {
                var row = CreateRow(recognizerResults, testedCultures, benchmarkType, i);
                result.Add(row);
            }

            return result;
        }

        private static List<string> InitilaizeColumns(List<string> testedCultures)
        {
            var columns = new List<string>() { "" };
            columns.AddRange(testedCultures);
            return columns;
        }

        private static List<string> CreateRow(List<BenchmarkResult> recognizerResults, List<string> testedCultures, BenchmarkType benchmarkType, int iteration)
        {
            var row = new List<string>() { iteration.ToString() };
            Console.WriteLine(recognizerResults.Count);
            foreach (var r in recognizerResults)
                Console.WriteLine(r.Culture + " " + r.Iteration + " " + r.Type.ToString() + " " + r.Value.ToString());
            foreach (var culture in testedCultures)
            { 
                var currentResult = recognizerResults.Where(r => r.Iteration == iteration && r.Culture == culture && r.Type == benchmarkType).First();
                row.Add(currentResult.Value.ToString());
            }
            return row;
        }

        public static List<BenchmarkResult> To(List<List<string>> table)
        {
            var benchmarkResults = new List<BenchmarkResult>();

            for (int i = 1; i < table.Count; i++)
            {
                var currentRow = table[i];
                for (int j = 1; j < table[i].Count; j++)
                {

                }
            }

            return benchmarkResults;
        }
    }
}
