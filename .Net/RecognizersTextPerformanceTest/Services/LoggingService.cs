using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Services
{
    public class LoggingService
    {
        private List<ILogger> _loggers;

        public LoggingService()
        {
            _loggers = new List<ILogger>();
        }

        public void RegisterLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }
        
        public void Log(string operationName, string message)
        {
            foreach (var logger in _loggers)
                logger.Log(operationName, message);
        }
    }
}
