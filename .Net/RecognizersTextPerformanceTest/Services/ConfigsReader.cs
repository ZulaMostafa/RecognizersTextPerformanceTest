using RecognizersTextPerformanceTest.Helpers;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Services
{
    public static class ConfigsReader
    {
        public static ConfigModel LoadApplicationConfigs()
        {
            var filePath = Path.Combine(Constants.ConfigsFileDirectory, Constants.ConfigsFileName);
            if (File.Exists(filePath))
            {
                var configsFile = File.ReadAllText(filePath);
                return JsonHandler.DeserializeObject<ConfigModel>(configsFile, Constants.ConfigsFileName);
            }
            // TODO: handle exception
            throw new Exception("File not found");
        }
    }
}
