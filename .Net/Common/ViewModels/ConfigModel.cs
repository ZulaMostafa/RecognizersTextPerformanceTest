using Newtonsoft.Json;

namespace Common.ViewModels
{
    public class ConfigModel
    {
        [JsonProperty("testsRootPath")]
        public string TestsRootPath { get; set; }

        [JsonProperty("culturesOption")]
        public string CulturesOption { get; set; }

        [JsonProperty("recognizersOption")]
        public string RecognizersOption { get; set; }

        [JsonProperty("iterationCount")]
        public int IterationCount { get; set; }

        [JsonProperty("operationName")]
        public string OperationName { get; set; }

        [JsonProperty("pythonPath")]
        public string PythonPath { get; set; }

        [JsonProperty("scriptPath")]
        public string ScriptPath { get; set; }
    }
}
