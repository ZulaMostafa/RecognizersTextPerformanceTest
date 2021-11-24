using Common.enums;
using System.Collections.Generic;

namespace Common
{
    class Constants
    {
        public const string ConfigsFileDirectory = ".";
        public const string ConfigsFileName = "configs.json";
        public const string LogsFileDirectory = "logs";
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
        public static readonly List<Recognizers> recognizers = new List<Recognizers>()
        {
            Recognizers.Choice,
            Recognizers.DateTime,
            Recognizers.Number,
            Recognizers.NumberWithUnit,
            Recognizers.Sequence
        };
    }
}
