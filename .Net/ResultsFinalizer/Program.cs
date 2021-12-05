using Common.Helpers;
using Common.Services;
using ResultsFinalizer.Helpers;
using System;
using System.IO;

namespace ResultsFinalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainDirectory = EnvironmentVariables.GetMainDirectory();

            var currentReleasePath = Path.Combine(mainDirectory, EnvironmentVariables.GetCurrentReleaseDirectory());
            var currentReleaseResults = CSVToBenchmarkResults.Load(currentReleasePath);

            var nextReleasePath = Path.Combine(mainDirectory, EnvironmentVariables.GetNextReleaseDirectory());
            var nextReleaseResults = CSVToBenchmarkResults.Load(nextReleasePath);

            FinalResultsToCSV.Save(currentReleaseResults, nextReleaseResults, "results");
        }
    }
}

