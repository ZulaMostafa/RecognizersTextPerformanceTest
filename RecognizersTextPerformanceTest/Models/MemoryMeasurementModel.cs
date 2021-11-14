using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Models
{
    public class MemoryMeasurementModel: IMeasurementModel
    {
        public long totalMemory;

        public MemoryMeasurementModel()
        {
            totalMemory = 0;
        }

        public void Measure(Action actionToBeDone)
        {
            // TODO
        }

        public object getResult()
        {
            return totalMemory;
        }
    }
}
