using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Helpers
{
    public static class TestsReader
    {
        public static List<string> ReadTests(string rootPath, string culture)
        {
            // get culture dataset path
            var path = Path.Combine(rootPath, $"{culture}.json");

            // read json text
            var jsonData = File.ReadAllBytes(path);

            // convert to text
            var jsonText = Encoding.UTF8.GetString(jsonData);

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
