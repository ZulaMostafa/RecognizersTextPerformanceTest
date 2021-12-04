using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.ViewModels;

namespace Common.Helpers
{
    public class SummaryTextToBenchmarkResults
    {
        public static BenchmarkResults Convert(string path)
        {
            var result = new BenchmarkResults(1);

            var text = File.ReadAllLines(path);
            for (int i = 2; i < text.Length; i += 2)
            {
                var curr = text[i].Split('@');
                var culture = curr[3];
                var recognizer = curr[4];

                var timeInNano = double.Parse(remove(curr[5]));
                var timeInSecond = timeInNano / 1000000;

                var memoryInKbs = double.Parse(remove(curr[13]));

                var r = new BenchmarksMetrics()
                {
                    Time = timeInSecond,
                    Memory = memoryInKbs
                };

                if (recognizer == "DateTime")
                    result.DateTimeRecognizerResults[0].Add(culture, r);
                if (recognizer == "Sequence")
                    result.SeqeuenceRecognizerResults[0].Add(culture, r);
                if (recognizer == "Number")
                    result.NumberRecognizerResults[0].Add(culture, r);
                if (recognizer == "NumberWithUnit")
                    result.NumberWithUnitRecognizerResults[0].Add(culture, r);
                if (recognizer == "Choice")
                    result.ChoiceRecognizerResults[0].Add(culture, r);
            }

            return result;
        }

        private static string remove(string x)
        {
            string ret = "";
            foreach (var w in x)
            {
                if (w == '.')
                    break;
                if (w >= '0' && w <= '9')
                    ret += w;
            }
            return ret;
        }
    }
}
