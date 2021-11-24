using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner
{
    public static class Constants
    {
        public const string ConfigsFileName = "configs.json";
        public static readonly List<string> cultures = new List<string>()
        {
            "Arabic",
            "Bulgarian",
            "Chinese",
            "Dutch",
            "English",
            "French",
            "German",
            "Hindi",
            "Italian",
            "Japanese",
            "Korean",
            "Portuguese",
            "Spanish",
            "Swedish",
            "Turkish"
        };
        public static readonly List<string> recognizers = new List<string>()
        {
            "Choice",
            "DateTime",
            "Number",
            "NumberWithUnit",
            "Sequence"
        };
    }
}
