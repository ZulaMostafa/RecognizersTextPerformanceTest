using ScriptRunner.Helpers;
using ScriptRunner.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Services
{
    public static class ConfigsReader
    {
        public static ConfigModel LoadApplicationConfigs()
        {
            if (File.Exists(Constants.ConfigsFileName))
            {
                var configsFile = File.ReadAllText(Constants.ConfigsFileName);
                return JsonHandler.DeserializeObject<ConfigModel>(configsFile, Constants.ConfigsFileName);
            }
            // TODO: handle exception
            throw new Exception("File not found");
        }
    }
}
