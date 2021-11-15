using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Models.Logger
{
    public class ConsoleLogger: ILogger
    {
        public void Log(string operationName, string message)
        {
            Console.WriteLine(string.Join('\n', $"{operationName}: ", message));
        }
    }
}
