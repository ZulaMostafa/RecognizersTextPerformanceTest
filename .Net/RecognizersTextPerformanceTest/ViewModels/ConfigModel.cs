using Microsoft.Recognizers.Text;
using Newtonsoft.Json;
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
        public List<Culture> cultures { get; set; }

        [JsonProperty("recognizers")]
        public List<string> recognizers { get; set; }
    }
}
