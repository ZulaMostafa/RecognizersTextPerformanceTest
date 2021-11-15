using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Models.Logger
{
    public class FileLogger: ILogger
    {
        public void Log(string operationName, string message)
        {
            // TODO: handle directory not found
            if (!Directory.Exists(Constants.LogsFileDirectory))
                Directory.CreateDirectory(Constants.LogsFileDirectory);

            var path = Path.Combine(Constants.LogsFileDirectory, $"{operationName}.txt");

            if (!File.Exists(path))
            {
                File.WriteAllText(path, message);
                return;
            }

            var currentText = File.ReadAllText(path);

            File.WriteAllText(path, string.Join('\n', currentText, message));
        }
    }
}
