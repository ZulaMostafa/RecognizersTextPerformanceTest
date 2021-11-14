using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using RecognizersTextPerformanceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Services
{
    public class TextRecognizersClient: ITextRecognizerClient
    {
        private List<IModel> _models;
        public TextRecognizersClient(List<string> languages)
        {
            _models = new List<IModel>();

            foreach (var language in languages)
            {
                AddNumberModels(language);
                // TODO: add rest of the models
            }
        }

        private void AddNumberModels(string language)
        {
            var numberRecognizer = new NumberRecognizer(language);
            _models.Add(numberRecognizer.GetNumberModel());
            _models.Add(numberRecognizer.GetNumberRangeModel());
            _models.Add(numberRecognizer.GetOrdinalModel());
            _models.Add(numberRecognizer.GetPercentageModel());
        }

        public void RunTest(string test)
        {
            foreach (var model in _models)
                model.Parse(test);
        }
    }
}
