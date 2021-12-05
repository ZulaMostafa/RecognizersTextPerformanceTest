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

            var currentReleasePath = Path.Combine(mainDirectory, "CurrentRelease");
            var currentReleaseResults = CSVToBenchmarkResults.Load(currentReleasePath);
            Console.WriteLine("Current release tests loaded");

            var nextReleasePath = Path.Combine(mainDirectory, "NextRelease");
            var nextReleaseResults = CSVToBenchmarkResults.Load(nextReleasePath);
            Console.WriteLine("next release tests loaded");

            FinalResultsToCSV.Save(currentReleaseResults, nextReleaseResults, "results");
            Console.Write("Results Saved");
        }
    }
}

