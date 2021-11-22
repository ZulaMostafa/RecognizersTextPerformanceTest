using RecognizersTextPerformanceTest.enums;
using System.Collections.Generic;

namespace RecognizersTextPerformanceTest.ViewModels
{
    public class PerformanceResults
    {
        public List<Dictionary<string, PerformanceMetrics>> ChoiceRecognizerResults;
        public List<Dictionary<string, PerformanceMetrics>> SeqeuenceRecognizerResults;
        public List<Dictionary<string, PerformanceMetrics>> DateTimeRecognizerResults;
        public List<Dictionary<string, PerformanceMetrics>> NumberRecognizerResults;
        public List<Dictionary<string, PerformanceMetrics>> NumberWithUnitRecognizerResults;



        public PerformanceResults(int iterationCount)
        {
            ChoiceRecognizerResults = new List<Dictionary<string, PerformanceMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                ChoiceRecognizerResults.Add(new Dictionary<string, PerformanceMetrics>());

            SeqeuenceRecognizerResults = new List<Dictionary<string, PerformanceMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                SeqeuenceRecognizerResults.Add(new Dictionary<string, PerformanceMetrics>());

            DateTimeRecognizerResults = new List<Dictionary<string, PerformanceMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                DateTimeRecognizerResults.Add(new Dictionary<string, PerformanceMetrics>());

            NumberRecognizerResults = new List<Dictionary<string, PerformanceMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                NumberRecognizerResults.Add(new Dictionary<string, PerformanceMetrics>());

            NumberWithUnitRecognizerResults = new List<Dictionary<string, PerformanceMetrics>>(iterationCount);
            for (int i = 0; i < iterationCount; i++)
                NumberWithUnitRecognizerResults.Add(new Dictionary<string, PerformanceMetrics>());
        }

        public Dictionary<string, PerformanceMetrics> GetDictionary(Recognizers recongizer, int iteration)
        {
            switch (recongizer)
            {
                case Recognizers.Choice:
                    return ChoiceRecognizerResults[iteration];
                case Recognizers.Sequence:
                    return SeqeuenceRecognizerResults[iteration];
                case Recognizers.DateTime:
                    return DateTimeRecognizerResults[iteration];
                case Recognizers.Number:
                    return NumberRecognizerResults[iteration];
                case Recognizers.NumberWithUnit:
                    return NumberWithUnitRecognizerResults[iteration];
            }
            return null;
        }
    }
}
