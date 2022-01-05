using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Helpers
{
    public static class ConfigurationInitalizer
    {

        public static List<Recognizers> InitalizeRecognizersList(string recognizersOptions)
        {
            if (recognizersOptions.ToLower() == "all")
                return RecognizerTextValues.recognizers;

            var recognizers = new List<Recognizers>() { (Recognizers)Enum.Parse(typeof(Recognizers), recognizersOptions) };
            return recognizers;
        }

        public static List<string> InitalizeCulturesList(string culturesOptions)
        {
            if (culturesOptions.ToLower() == "all")
                return RecognizerTextValues.cultures;

            var cultures = new List<string>() { culturesOptions };
            return cultures;
        }
    }
}
