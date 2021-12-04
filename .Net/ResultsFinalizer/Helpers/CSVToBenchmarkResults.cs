using Common;
using Common.enums;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultsFinalizer.Helpers
{
    public static class CSVToBenchmarkResults
    {
        public static BenchmarkResults Load(string path)
        {
            var result = new BenchmarkResults(1);
            LoadChoiceResults(result, path);
            LoadSequenceResults(result, path);
            LoadNumberResults(result, path);
            LoadNumberWithUnitResults(result, path);
            LoadDateTimeResults(result, path);
            return result;
        }

        private static void LoadChoiceResults(BenchmarkResults benchmarkResults, string path)
            => benchmarkResults.ChoiceRecognizerResults = LoadRecognizerResults(path, Recognizers.Choice);

        private static void LoadSequenceResults(BenchmarkResults benchmarkResults, string path)
            => benchmarkResults.SeqeuenceRecognizerResults = LoadRecognizerResults(path, Recognizers.Sequence);

        private static void LoadNumberResults(BenchmarkResults benchmarkResults, string path)
            => benchmarkResults.NumberRecognizerResults = LoadRecognizerResults(path, Recognizers.Number);

        private static void LoadNumberWithUnitResults(BenchmarkResults benchmarkResults, string path)
            => benchmarkResults.NumberWithUnitRecognizerResults = LoadRecognizerResults(path, Recognizers.NumberWithUnit);

        private static void LoadDateTimeResults(BenchmarkResults benchmarkResults, string path)
            => benchmarkResults.DateTimeRecognizerResults = LoadRecognizerResults(path, Recognizers.DateTime);

        private static List<Dictionary<string, BenchmarksMetrics>> LoadRecognizerResults(string path, Recognizers recognizer)
        {
            var result = new List<Dictionary<string, BenchmarksMetrics>>();
            result.Add(new Dictionary<string, BenchmarksMetrics>());

            var timeFile = Path.Combine(path, recognizer.ToString(), $"{recognizer}-time.csv");
            var timeData = File.ReadAllLines(timeFile);
            var splittedtimeData = timeData.Select(line => line.Split(',')).ToList();
            for (int i = 0; i < Constants.cultures.Count; i++)
            {
                var currentCulture = splittedtimeData.First()[i + 1];
                var currentValue = double.Parse(splittedtimeData.Last()[i + 1]);
                var benchmarkMetrics = new BenchmarksMetrics()
                {
                    Time = currentValue
                };
                result.First().Add(currentCulture, benchmarkMetrics);
            }

            var memoryFile = Path.Combine(path, recognizer.ToString(), $"{recognizer}-memory.csv");
            var memoryData = File.ReadAllLines(memoryFile);
            var splittedMemoryData = memoryData.Select(line => line.Split(',')).ToList();
            for (int i = 0; i < Constants.cultures.Count; i++)
            {
                var currentCulture = splittedMemoryData.First()[i + 1];
                var currentValue = double.Parse(splittedMemoryData.Last()[i + 1]);
                result.First()[currentCulture].Memory = currentValue;
            }

            return result;
        }
    }
}
