using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface IMeasurementModel
    {
        public void Measure(Action actionToBeDone);
        public object getResult();
    }
}
