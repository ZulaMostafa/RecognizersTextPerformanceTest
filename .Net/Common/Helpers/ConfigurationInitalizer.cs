using Common.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class ConfigurationInitalizer
    {

        public static List<Recognizers> InitalizeRecognizersList(string recognizersOptions)
        {
            if (recognizersOptions.ToLower() == "all")
                return Constants.recognizers;

            var recognizers = new List<Recognizers>() { (Recognizers)Enum.Parse(typeof(Recognizers), recognizersOptions) };
            return recognizers;
        }

        public static List<string> InitalizeCulturesList(string culturesOptions)
        {
            if (culturesOptions.ToLower() == "all")
                return Constants.cultures;

            var cultures = new List<string>() { culturesOptions };
            return cultures;
        }
    }
}
