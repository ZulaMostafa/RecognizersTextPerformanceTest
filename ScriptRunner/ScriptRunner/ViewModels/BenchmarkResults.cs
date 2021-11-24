using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.ViewModels
{
    public class BenchmarkResults
    {
        public List<Dictionary<string, BenchmarksMetrics>> ChoiceRecognizerResults;
        public List<Dictionary<string, BenchmarksMetrics>> SeqeuenceRecognizerResults;
        public List<Dictionary<string, BenchmarksMetrics>> DateTimeRecognizerResults;
        public List<Dictionary<string, BenchmarksMetrics>> NumberRecognizerResults;
        public List<Dictionary<string, BenchmarksMetrics>> NumberWithUnitRecognizerResults;

        public BenchmarkResults(int iterationCount)
        {
            ChoiceRecognizerResults = new List<Dictionary<string, BenchmarksMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                ChoiceRecognizerResults.Add(new Dictionary<string, BenchmarksMetrics>());

            SeqeuenceRecognizerResults = new List<Dictionary<string, BenchmarksMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                SeqeuenceRecognizerResults.Add(new Dictionary<string, BenchmarksMetrics>());

            DateTimeRecognizerResults = new List<Dictionary<string, BenchmarksMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                DateTimeRecognizerResults.Add(new Dictionary<string, BenchmarksMetrics>());

            NumberRecognizerResults = new List<Dictionary<string, BenchmarksMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                NumberRecognizerResults.Add(new Dictionary<string, BenchmarksMetrics>());

            NumberWithUnitRecognizerResults = new List<Dictionary<string, BenchmarksMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                NumberWithUnitRecognizerResults.Add(new Dictionary<string, BenchmarksMetrics>());
        }

        public Dictionary<string, BenchmarksMetrics> GetDictionary(string recongizer, int iteration)
        {
            switch (recongizer)
            {
                case "Choice":
                    return ChoiceRecognizerResults[iteration];
                case "Sequence":
                    return SeqeuenceRecognizerResults[iteration];
                case "DateTime":
                    return DateTimeRecognizerResults[iteration];
                case "Number":
                    return NumberRecognizerResults[iteration];
                case "NumberWithUnit":
                    return NumberWithUnitRecognizerResults[iteration];
            }
            return null;
        }
    }
}
