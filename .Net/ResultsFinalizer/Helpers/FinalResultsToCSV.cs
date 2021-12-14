using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;
using Common.enums;
using Common;
using System.IO;
using Common.Services;

namespace ResultsFinalizer.Helpers
{
    public static class FinalResultsToCSV
    {
        public static void Save(BenchmarkResults currentBuildResults, BenchmarkResults nextBuildResults)
        {
            var operationName = EnvironmentVariables.GetOperationName();
            var mainDirectory = EnvironmentVariables.GetMainDirectory();
            var workingDirectory = Path.Combine(mainDirectory, operationName, "FinalResults");

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var timeResults = GetTimeResults(currentBuildResults, nextBuildResults);
            var timeFilePath = Path.Combine(workingDirectory, "time.csv");
            File.WriteAllText(timeFilePath, timeResults.ToString());

            var memoryResults = GetMemoryResults(currentBuildResults, nextBuildResults);
            var memoryFilePath = Path.Combine(workingDirectory, "memory.csv");
            File.WriteAllText(memoryFilePath, memoryResults.ToString());
        }

        public static string GetTimeResults(BenchmarkResults currentBuildResults, BenchmarkResults nextBuildResults)
        {
            var timeResult = new StringBuilder();
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.ChoiceRecognizerResults, nextBuildResults.ChoiceRecognizerResults, "Choice"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.SeqeuenceRecognizerResults, nextBuildResults.SeqeuenceRecognizerResults, "Sequence"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.DateTimeRecognizerResults, nextBuildResults.DateTimeRecognizerResults, "DateTime"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.NumberRecognizerResults, nextBuildResults.NumberRecognizerResults, "Number"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.NumberWithUnitRecognizerResults, nextBuildResults.NumberWithUnitRecognizerResults, "NumberWithUnit"));
            return timeResult.ToString();
        }

        public static string GetMemoryResults(BenchmarkResults currentBuildResults, BenchmarkResults nextBuildResults)
        {
            var memoryResult = new StringBuilder();
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.ChoiceRecognizerResults, nextBuildResults.ChoiceRecognizerResults, "Choice"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.SeqeuenceRecognizerResults, nextBuildResults.SeqeuenceRecognizerResults, "Sequence"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.DateTimeRecognizerResults, nextBuildResults.DateTimeRecognizerResults, "DateTime"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.NumberRecognizerResults, nextBuildResults.NumberRecognizerResults, "Number"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.NumberWithUnitRecognizerResults, nextBuildResults.NumberWithUnitRecognizerResults, "NumberWithUnit"));
            return memoryResult.ToString();
        }

        private static string GetRecognizerTimeResults(List<Dictionary<string, BenchmarksMetrics>> currentRecognizerResults, List<Dictionary<string, BenchmarksMetrics>> upcomingRecognizerResults, string recognizer)
            => GetRecognizerResults(currentRecognizerResults, upcomingRecognizerResults, recognizer, "time");

        private static string GetRecognizerMemoryResults(List<Dictionary<string, BenchmarksMetrics>> currentRecognizerResults, List<Dictionary<string, BenchmarksMetrics>> upcomingRecognizerResults, string recognizer)
           => GetRecognizerResults(currentRecognizerResults, upcomingRecognizerResults, recognizer, "memory");

        public static string GetRecognizerResults(List<Dictionary<string, BenchmarksMetrics>> currentRecognizerResults, List<Dictionary<string, BenchmarksMetrics>> upcomingRecognizerResults, string recognizer, string benchmarkType)
        {
            var result = new StringBuilder($"{recognizer}\n");
            result.Append("t,");
            foreach (var culture in Constants.cultures)
                result.Append($"{culture},");
            result.AppendLine();

            result.Append("Current Build,");
            foreach (var culture in Constants.cultures)
            {
                if (benchmarkType == "time")
                    result.Append($"{currentRecognizerResults[0][culture].Time},");
                else if (benchmarkType == "memory")
                    result.Append($"{currentRecognizerResults[0][culture].Memory},");
            }
            result.AppendLine();

            result.Append("Upcoming Build,");
            foreach (var culture in Constants.cultures)
            {
                if (benchmarkType == "time")
                    result.Append($"{upcomingRecognizerResults[0][culture].Time},");
                else if (benchmarkType == "memory")
                    result.Append($"{upcomingRecognizerResults[0][culture].Memory},");
            }
            result.AppendLine();

            result.Append("Difference,");
            foreach (var culture in Constants.cultures)
            {
                if (benchmarkType == "time")
                    result.Append($"{Math.Round(upcomingRecognizerResults[0][culture].Time - currentRecognizerResults[0][culture].Time, 3)},");
                else if (benchmarkType == "memory")
                    result.Append($"{upcomingRecognizerResults[0][culture].Memory - currentRecognizerResults[0][culture].Memory},");
            }
            result.AppendLine();

            result.Append("Percentage,");
            foreach (var culture in Constants.cultures)
            {
                if (benchmarkType == "time")
                    result.Append($"{CalculatePercentage(currentRecognizerResults[0][culture].Time, upcomingRecognizerResults[0][culture].Time)},");
                else if (benchmarkType == "memory")
                    result.Append($"{CalculatePercentage(currentRecognizerResults[0][culture].Memory, upcomingRecognizerResults[0][culture].Memory)},");
            }
            result.AppendLine();

            result.AppendLine();
            return result.ToString();
        }

        private static double CalculatePercentage(double x, double y)
        {
            var difference = y - x;
            var percentage = (difference / x) * 100;
            var rounded = Math.Round(percentage, 3);
            return rounded;
        }
    }
}
