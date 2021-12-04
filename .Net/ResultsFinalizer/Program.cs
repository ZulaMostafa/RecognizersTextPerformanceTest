using Common.Helpers;
using ResultsFinalizer.Helpers;
using System;

namespace ResultsFinalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var current = SummaryTextToBenchmarkResults.Convert("current.txt");
            var upcoming = SummaryTextToBenchmarkResults.Convert("upcoming2.txt");

            ResultsToCSV x = new ResultsToCSV(1);
            x.SaveAllResultsAsCSV(current, "current");
            x.SaveAllResultsAsCSV(upcoming, "upcoming");

            var currentBuildPath = "current";
            var upcomingBuildPath = "upcoming";

            var currentReleaseResults = CSVToBenchmarkResults.Load(currentBuildPath);
            var upcomingReleaseResults = CSVToBenchmarkResults.Load(upcomingBuildPath);

            FinalResultsToCSV.Save(currentReleaseResults, upcomingReleaseResults, "results");
        }
    }
}

