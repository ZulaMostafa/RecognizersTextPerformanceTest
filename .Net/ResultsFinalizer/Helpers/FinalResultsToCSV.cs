﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;
using Common.enums;
using Common;
using System.IO;

namespace ResultsFinalizer.Helpers
{
    public static class FinalResultsToCSV
    {
        public static void Save(BenchmarkResults currentBuildResults, BenchmarkResults nextBuildResults, string operationName)
        {
            Directory.CreateDirectory(operationName);

            var timeResult = new StringBuilder();
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.ChoiceRecognizerResults, nextBuildResults.ChoiceRecognizerResults, "Choice"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.SeqeuenceRecognizerResults, nextBuildResults.SeqeuenceRecognizerResults, "Sequence"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.DateTimeRecognizerResults, nextBuildResults.DateTimeRecognizerResults, "DateTime"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.NumberRecognizerResults, nextBuildResults.NumberRecognizerResults, "Number"));
            timeResult.Append(GetRecognizerTimeResults(currentBuildResults.NumberWithUnitRecognizerResults, nextBuildResults.NumberWithUnitRecognizerResults, "NumberWithUnit"));
            var timeFilePath = Path.Combine(operationName, "time.csv");
            File.WriteAllText(timeFilePath, timeResult.ToString());

            var memoryResult = new StringBuilder();
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.ChoiceRecognizerResults, nextBuildResults.ChoiceRecognizerResults, "Choice"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.SeqeuenceRecognizerResults, nextBuildResults.SeqeuenceRecognizerResults, "Sequence"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.DateTimeRecognizerResults, nextBuildResults.DateTimeRecognizerResults, "DateTime"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.NumberRecognizerResults, nextBuildResults.NumberRecognizerResults, "Number"));
            memoryResult.Append(GetRecognizerMemoryResults(currentBuildResults.NumberWithUnitRecognizerResults, nextBuildResults.NumberWithUnitRecognizerResults, "NumberWithUnit"));
            var memoryFilePath = Path.Combine(operationName, "memory.csv");
            File.WriteAllText(memoryFilePath, memoryResult.ToString());
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
                    result.Append($"{upcomingRecognizerResults[0][culture].Time - currentRecognizerResults[0][culture].Time},");
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