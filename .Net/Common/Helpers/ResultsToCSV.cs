using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Helpers
{
    public class ResultsToCSV
    {
        private int _iterationCount;
        public ResultsToCSV(int iterationCount)
        {
            _iterationCount = iterationCount;
        }
        public void SaveAllResultsAsCSV(BenchmarkResults benchmarkeResults, string operationName)
        {
            Directory.CreateDirectory(operationName);

            SaveResults(operationName, "Choice", benchmarkeResults.ChoiceRecognizerResults);
            SaveResults(operationName, "Sequence", benchmarkeResults.SeqeuenceRecognizerResults);
            SaveResults(operationName, "DateTime", benchmarkeResults.DateTimeRecognizerResults);
            SaveResults(operationName, "Number", benchmarkeResults.NumberRecognizerResults);
            SaveResults(operationName, "NumberWithUnit", benchmarkeResults.NumberWithUnitRecognizerResults);
        }

        public void SaveResults(string operationName, string recognizer, List<Dictionary<string, BenchmarksMetrics>> list)
        {
            var directtory = Path.Combine(operationName, recognizer);
            Directory.CreateDirectory(directtory);

            var memoryCSVResult = GetMemoryResultAsCSV(list);
            var memoryFilePath = Path.Combine(directtory, $"{recognizer}-memory.csv");
            File.WriteAllText(memoryFilePath, memoryCSVResult);

            var timeCSVResult = GetTimeResultAsCSV(list);
            var timefilePath = Path.Combine(directtory, $"{recognizer}-time.csv");
            File.WriteAllText(timefilePath, timeCSVResult);
        }

        public string GetMemoryResultAsCSV(List<Dictionary<string, BenchmarksMetrics>> list)
        {
            var result = new StringBuilder("t,");

            foreach (var culture in Constants.cultures)
                result.Append($"{culture},");

            result.AppendLine();

            for (int i = 1; i <= _iterationCount; i++)
            {
                result.Append($"itr {i},");

                foreach (var culture in Constants.cultures)
                {
                    var currentIterationList = list[i - 1];
                    if (!currentIterationList.ContainsKey(culture))
                    {
                        result.Append("0,");
                        continue;
                    }

                    var currentMetrics = currentIterationList[culture];

                    var memory = Math.Round(currentMetrics.Memory, 3);

                    result.Append($"{memory},");
                }

                result.AppendLine();
            }

            result.Append("avg,");

            foreach (var culture in Constants.cultures)
            {
                if (!list[0].ContainsKey(culture))
                {
                    result.Append("0,");
                    continue;
                }

                var filteredList = list.Select(x => x[culture].Memory);
                var sum = filteredList.Sum();
                var max = filteredList.Max();
                var min = filteredList.Min();
                var average = (sum - (min + max)) / (_iterationCount - 2);
                var roundedAverage = Math.Round(average, 3);
                result.Append($"{roundedAverage},");
            }

            return result.ToString();
        }

        public string GetTimeResultAsCSV(List<Dictionary<string, BenchmarksMetrics>> list)
        {
            var result = new StringBuilder("t,");

            foreach (var culture in Constants.cultures)
                result.Append($"{culture},");

            result.AppendLine();

            for (int i = 1; i <= _iterationCount; i++)
            {
                result.Append($"itr {i},");

                foreach (var culture in Constants.cultures)
                {
                    var currentIterationList = list[i - 1];
                    if (!currentIterationList.ContainsKey(culture))
                    {
                        result.Append("0,");
                        continue;
                    }

                    var currentMetrics = currentIterationList[culture];

                    var time = Math.Round(currentMetrics.Time, 3);

                    result.Append($"{time},");
                }

                result.AppendLine();
            }

            result.Append("avg,");

            foreach (var culture in Constants.cultures)
            {
                if (!list[0].ContainsKey(culture))
                {
                    result.Append("0,");
                    continue;
                }

                var filteredList = list.Select(x => x[culture].Time);
                var sum = filteredList.Sum();
                var max = filteredList.Max();
                var min = filteredList.Min();
                var average = (sum - (min + max)) / (_iterationCount - 2);
                var roundedAverage = Math.Round(average, 3);
                result.Append($"{roundedAverage},");
            }

            return result.ToString();
        }
    }
}
