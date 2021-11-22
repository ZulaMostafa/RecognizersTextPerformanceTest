using Newtonsoft.Json;

namespace RecognizersTextPerformanceTest.ViewModels
{
    public class ConfigModel
    {
        [JsonProperty("rootPath")]
        public string RootPath { get; set; }

        [JsonProperty("cultures")]
        public string CulturesOption { get; set; }

        [JsonProperty("recognizers")]
        public string RecognizersOption { get; set; }

        [JsonProperty("iterationCount")]
        public int IterationCount { get; set; }

        [JsonProperty("operationName")]
        public string OperationName { get; set; }
    }
}
