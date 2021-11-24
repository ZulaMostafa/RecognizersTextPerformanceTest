using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.ViewModels
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
