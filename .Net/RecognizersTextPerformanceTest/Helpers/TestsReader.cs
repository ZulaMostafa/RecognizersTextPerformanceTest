using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecognizersTextPerformanceTest.Helpers
{
    public static class TestsReader
    {
        public static List<string> ReadTests(string rootPath, string culture)
        {
            // get culture dataset path
            var path = Path.Combine(rootPath, $"{culture}.json");

            // read json text
            var jsonText = File.ReadAllText(path);

            // parse into json array
            var jArray = JArray.Parse(jsonText);

            // get test jObject
            var jObjects = jArray.Select(testObject => testObject as JObject).ToList();

            // get tests input
            var inputs = jObjects.Select(testObject => testObject["Input"].ToString()).ToList();

            return inputs;
        }
    }
}
