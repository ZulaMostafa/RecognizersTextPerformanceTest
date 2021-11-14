using RecognizersTextPerformanceTest.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace RecognizersTextPerformanceTest.Services
{
    public class TestsReader<T> : IFilesReader<T>
    {
        public ITestParser _parser;

        public TestsReader(ITestParser parser)
        {
            _parser = parser;
        }
        public List<T> LoadTests(string path)
        {
            var input = File.ReadAllText(path);
            var testsInFile = _parser.Parse<T>(input);

            return testsInFile;
        }
    }
}
