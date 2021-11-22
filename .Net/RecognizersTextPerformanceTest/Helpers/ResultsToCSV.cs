using RecognizersTextPerformanceTest.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecognizersTextPerformanceTest.Helpers
{
    public class ResultsToCSV
    {
        private int _iterationCount;
        public ResultsToCSV(int iterationCount)
        {
            _iterationCount = iterationCount;
        }
        public void SaveAllResultsAsCSV(PerformanceResults performanceResults, string operationName)
        {
            Directory.CreateDirectory(operationName);

            SaveResults(operationName, "Choice", performanceResults.ChoiceRecognizerResults);
            SaveResults(operationName, "Sequence", performanceResults.SeqeuenceRecognizerResults);
            SaveResults(operationName, "DateTime", performanceResults.DateTimeRecognizerResults);
            SaveResults(operationName, "Number", performanceResults.NumberRecognizerResults);
            SaveResults(operationName, "NumberWithUnit", performanceResults.NumberRecognizerResults);
        }

        public void SaveResults(string operationName, string recognizer, List<Dictionary<string, PerformanceMetrics>> list)
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

        public string GetMemoryResultAsCSV(List<Dictionary<string, PerformanceMetrics>> list)
        {
            var result = new StringBuilder("t,");

            for (int i = 1; i <= _iterationCount; i++)
            {
                result.Append($"itr {i}");
                result.Append(",");
            }
            result.AppendLine("avg");

            foreach (var culture in Constants.cultures)
            {
                result.Append($"{culture},");

                var memoryValues = new List<long>();
                for (int i = 0; i < _iterationCount; i++)
                {
                    var currentIterationList = list[i];
                    if (!currentIterationList.ContainsKey(culture))
                        continue;

                    var currentMetrics = currentIterationList[culture];

                    result.Append($"{currentMetrics.Memory},");

                    memoryValues.Add(currentMetrics.Memory);
                }

                // calculate average
                memoryValues.Sort();
                var sum = memoryValues.Skip(1).Take(_iterationCount - 2).Sum();
                var avg = sum / (_iterationCount - 2);
                result.AppendLine($"{avg}");
            }

            return result.ToString();
        }

        public string GetTimeResultAsCSV(List<Dictionary<string, PerformanceMetrics>> list)
        {
            var result = new StringBuilder("t,");

            for (int i = 1; i <= _iterationCount; i++)
            {
                result.Append($"itr {i}");
                result.Append(",");
            }
            result.AppendLine("avg");

            foreach (var culture in Constants.cultures)
            {
                result.Append($"{culture},");

                var timeValues = new List<double>();
                for (int i = 0; i < _iterationCount; i++)
                {
                    var currentIterationList = list[i];
                    if (!currentIterationList.ContainsKey(culture))
                        continue;

                    var currentMetrics = currentIterationList[culture];

                    result.Append($"{currentMetrics.Time},");

                    timeValues.Add(currentMetrics.Time);
                }

                // calculate average
                timeValues.Sort();
                var sum = timeValues.Skip(1).Take(_iterationCount - 2).Sum();
                var avg = sum / (_iterationCount - 2);
                result.AppendLine($"{avg}");
            }

            return result.ToString();
        }
    }
}
