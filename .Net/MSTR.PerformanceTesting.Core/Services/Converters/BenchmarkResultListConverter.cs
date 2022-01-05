using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services.Converters
{
    public class BenchmarkResultListConverter
    {
        public List<List<string>> GetTable(List<BenchmarkResult> results, Recognizers recognizer, BenchmarkType benchmarkType)
        {
            var result = new List<List<string>>();

            // get configurations
            var testedCultures = ConfigurationInitalizer.InitalizeCulturesList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.CulturesConfiguration));
            var iterationsCount = int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.IterationsConfiguration));

            // initialize columns
            var columns = InitilaizeColumns(testedCultures);
            result.Add(columns);

            // get recognizer results
            var recognizerResults = results.Where(r => r.Recognizer == recognizer && r.Type == benchmarkType).ToList();

            // iterate over results
            for (int i = 1; i <= iterationsCount; i++)
            {
                var row = CreateRow(recognizerResults, testedCultures, i);
                result.Add(row);
            }

            return result;
        }

        private List<string> InitilaizeColumns(List<string> testedCultures)
        {
            var columns = new List<string>() { "" };
            columns.AddRange(testedCultures);
            return columns;
        }

        private List<string> CreateRow(List<BenchmarkResult> recognizerResults, List<string> testedCultures, int iteration)
        {
            var row = new List<string>() { iteration.ToString() };
            foreach (var culture in testedCultures)
            {
                var currentResult = recognizerResults.Where(r => r.Iteration == iteration && r.Culture == culture).First();
                row.Add(currentResult.Value.ToString());
            }
            return row;
        }
    } 
}
