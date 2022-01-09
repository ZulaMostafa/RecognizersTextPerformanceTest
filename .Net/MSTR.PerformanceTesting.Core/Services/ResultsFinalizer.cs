using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using MSTR.PerformanceTesting.Definitions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Services
{
    public class ResultsFinalizer
    {
        public List<List<string>> GetFinalResults(List<BenchmarkResult> previousReleaseResults, List<BenchmarkResult> nextReleaseResults, Recognizers recognizer, BenchmarkType benchmarkType, bool perTest)
        {
            var table = new List<List<string>>();

            // get configurations
            var testedCultures = ConfigurationInitalizer.InitalizeCulturesList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.CulturesConfiguration));

            // Initialize columns
            var columns = InitilaizeColumns(testedCultures);
            table.Add(columns);

            // filter results based on requested recognizer and benchmark type
            previousReleaseResults = previousReleaseResults.Where(results => results.Recognizer == recognizer && results.Type == benchmarkType).ToList();
            nextReleaseResults = nextReleaseResults.Where(results => results.Recognizer == recognizer && results.Type == benchmarkType).ToList();

            // get average Results
            List<BenchmarkResult> previousAverageResults;
            List<BenchmarkResult> nextAverageResults;

            if (perTest)
            {
                previousAverageResults = GetAverageResultsPerTests(previousReleaseResults, testedCultures);
                nextAverageResults = GetAverageResultsPerTests(nextReleaseResults, testedCultures);
            }
            else
            {
                 previousAverageResults = GetAverageResults(previousReleaseResults, testedCultures);
                 nextAverageResults = GetAverageResults(nextReleaseResults, testedCultures);
            }

            // create rows
            var previousReleaseRow = GetReleaseRow("Previous Release", testedCultures, previousAverageResults);
            var nextReleaseRow = GetReleaseRow("Next Release", testedCultures, nextAverageResults);
            var differenceRow = GetDifferenceRow("Difference", testedCultures, previousAverageResults, nextAverageResults);
            var percentageRow = GetPercentageRow("Percentage", testedCultures, previousAverageResults, nextAverageResults);

            // add rows
            table.Add(previousReleaseRow);
            table.Add(nextReleaseRow);
            table.Add(differenceRow);
            table.Add(percentageRow);

            return table;
        }
        private List<string> InitilaizeColumns(List<string> testedCultures)
        {
            var columns = new List<string>() { "" };
            columns.AddRange(testedCultures);
            return columns;
        }

        public List<BenchmarkResult> GetAverageResults(List<BenchmarkResult> benchmarkResults, List<string> testedCultures)
        {
            var averageResults = new List<BenchmarkResult>();
            foreach (var culture in testedCultures)
            {
                // get first iteration result and override its result
                var cultureResult = benchmarkResults.Where(result => result.Culture == culture && result.Iteration == 1).First();
                var cultureAverage = benchmarkResults.Where(result => result.Culture == culture).Average(result => result.Value);
                cultureResult.Value = cultureAverage;
                averageResults.Add(cultureResult);
            }
            return averageResults;
        }

        public List<BenchmarkResult> GetAverageResultsPerTests(List<BenchmarkResult> benchmarkResults, List<string> testedCultures)
        {
            var averageResults = new List<BenchmarkResult>();
            foreach (var culture in testedCultures)
            {
                var Directory = Path.Combine(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory), Directories.TestsDirectory);
                int cnt = TestsReader.ReadTests(Directory, culture).Count;

                // get first iteration result and override its result
                var cultureResult = benchmarkResults.Where(result => result.Culture == culture && result.Iteration == 1).First();
                var cultureAverage = benchmarkResults.Where(result => result.Culture == culture).Average(result => result.Value);
                cultureResult.Value = cultureAverage / (double)cnt;
                cultureResult.Value = Math.Round(cultureResult.Value, 5);
                averageResults.Add(cultureResult);
            }
            return averageResults;
        }

        public List<string> GetReleaseRow(string title, List<string> testedCultures, List<BenchmarkResult> averageResults)
        {
            var row = new List<string>() { title };
            foreach (var culture in testedCultures)
            {
                var cultureResults = averageResults.Where(result => result.Culture == culture).First();
                var average = cultureResults.Value;
                row.Add(average.ToString());
            }
            return row;
        }

        public List<string> GetDifferenceRow(string title, List<string> testedCultures, List<BenchmarkResult> averagePreviousReleaseResults, List<BenchmarkResult> averageNextReleaseResults)
        {
            var row = new List<string>() { title };
            foreach (var culture in testedCultures)
            {
                var culturePreviousResult = averagePreviousReleaseResults.Where(result => result.Culture == culture).First();
                var previousAverage = culturePreviousResult.Value;

                var cultureNextResult = averageNextReleaseResults.Where(result => result.Culture == culture).First();
                var NextAverage = cultureNextResult.Value;

                var differnce = NextAverage - previousAverage;
                differnce = Math.Round(differnce, 3);
                row.Add(differnce.ToString());
            }
            return row;
        }

        public List<string> GetPercentageRow(string title, List<string> testedCultures, List<BenchmarkResult> averagePreviousReleaseResults, List<BenchmarkResult> averageNextReleaseResults)
        {
            var row = new List<string>() { title };
            foreach (var culture in testedCultures)
            {
                var culturePreviousResult = averagePreviousReleaseResults.Where(result => result.Culture == culture).First();
                var previousAverage = culturePreviousResult.Value;

                var cultureNextResult = averageNextReleaseResults.Where(result => result.Culture == culture).First();
                var NextAverage = cultureNextResult.Value;

                var differnce = NextAverage - previousAverage;

                var percentage = (differnce / previousAverage) * 100f;
                percentage = Math.Round(percentage, 3);
                row.Add(percentage.ToString());
            }
            return row;
        }
    }
}
