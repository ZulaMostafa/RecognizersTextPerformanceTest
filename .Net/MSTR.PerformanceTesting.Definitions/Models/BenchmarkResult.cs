using MSTR.PerformanceTesting.Definitions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Definitions.Models
{
    public class BenchmarkResult
    {
        public int Iteration { get; set; }
        public string Culture { get; set; }
        public Recognizers Recognizer { get; set; }
        public BenchmarkType Type { get; set; }
        public double Value { get; set; }


    }
}
