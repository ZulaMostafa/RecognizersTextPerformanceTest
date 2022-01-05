using MSTR.PerformanceTesting.Definitions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Definitions.Consts
{
    public static class RecognizerTextValues
    {
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
