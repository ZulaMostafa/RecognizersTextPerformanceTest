using System.Collections.Generic;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface IFilesReader<T>
    {
        public List<T> LoadTests(string basePath);
    }
}
