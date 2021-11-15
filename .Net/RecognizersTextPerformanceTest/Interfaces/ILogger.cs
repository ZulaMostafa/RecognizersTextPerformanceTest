using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface ILogger
    {
        public void Log(string operationName, string message);
    }
}
