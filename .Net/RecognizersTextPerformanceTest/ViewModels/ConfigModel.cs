using Microsoft.Recognizers.Text;
using Newtonsoft.Json;
using RecognizersTextPerformanceTest.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.ViewModels
{
    public class ConfigModel
    {
        [JsonProperty("rootPath")]
        public string RootPath { get; set; }
        
        [JsonProperty("cultures")]
        public List<string> cultures { get; set; }

        [JsonProperty("recognizers")]
        public List<Recognizers> recognizers { get; set; }

        [JsonProperty("operationName")]
        public string OperationName { get; set; }
    }
}
