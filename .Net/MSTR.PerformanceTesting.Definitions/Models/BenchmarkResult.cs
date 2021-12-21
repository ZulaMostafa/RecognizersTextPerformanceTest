using MSTR.PerformanceTesting.Definitions.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Definitions.Models
{
    public class BenchmarkResult
    {
        [JsonProperty("Iteration")]
        public int Iteration { get; set; }

        [JsonProperty("Culture")]
        public string Culture { get; set; }

        [JsonProperty("Recognizer")]
        public Recognizers Recognizer { get; set; }

        [JsonProperty("Type")]
        public BenchmarkType Type { get; set; }

        [JsonProperty("Value")]
        public double Value { get; set; }
    }
}
