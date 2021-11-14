using RecognizersTextPerformanceTest.Interfaces;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Services
{
    public class TestsReader<T>: IFilesReader<T>
    {
        public ITestParser _parser;

        public TestsReader(ITestParser parser)
        {
            _parser = parser;
        }
        public List<T> LoadTests(string basePath)
        {
            var tests = new List<T>();

            CrawlDirectories(basePath, tests);

            return tests;
        }

        private void CrawlDirectories(string directory, List<T> tests)
        {
            foreach (var nestedDirectories in Directory.EnumerateDirectories(directory))
            {
                CrawlDirectories(nestedDirectories, tests);
            }

            ProcessDirectory(directory, tests);
        }

        private void ProcessDirectory(string directory, List<T> tests)
        {
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                var textInFile = File.ReadAllText(file);
                var testsInFile = _parser.Parse<T>(textInFile);

                tests.AddRange(testsInFile);
            }
        }

    }
}
