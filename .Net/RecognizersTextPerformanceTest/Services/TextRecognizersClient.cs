using Common.enums;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Choice;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.NumberWithUnit;
using Microsoft.Recognizers.Text.Sequence;
using RecognizersTextPerformanceTest.Interfaces;
using System.Collections.Generic;

namespace RecognizersTextPerformanceTest.Services
{
    public class TextRecognizersClient : ITextRecognizerClient
    {
        private List<IModel> _models;
        public TextRecognizersClient(string culture, Recognizers recognizer)
        {
            _models = new List<IModel>();

            switch (recognizer)
            {
                case Recognizers.Choice:
                    AddChoiceModels(culture);
                    break;
                case Recognizers.DateTime:
                    AddDateTimeModels(culture);
                    break;
                case Recognizers.Number:
                    AddNumberModels(culture);
                    break;
                case Recognizers.NumberWithUnit:
                    AddNumberWithUnitModels(culture);
                    break;
                case Recognizers.Sequence:
                    AddSequenceModels(culture);
                    break;
            }

        }
        private void AddChoiceModels(string culture)
        {
            var choiceRecognizr = new ChoiceRecognizer(culture);
            _models.Add(choiceRecognizr.GetBooleanModel());

            choiceRecognizr = null;
        }

        private void AddDateTimeModels(string culture)
        {
            var dateTimeRecognizer = new DateTimeRecognizer(culture);
            _models.Add(dateTimeRecognizer.GetDateTimeModel());

            dateTimeRecognizer = null;
        }

        private void AddNumberModels(string culture)
        {
            var numberRecognizer = new NumberRecognizer(culture);
            _models.Add(numberRecognizer.GetNumberModel());
            _models.Add(numberRecognizer.GetNumberRangeModel());
            _models.Add(numberRecognizer.GetOrdinalModel());
            _models.Add(numberRecognizer.GetPercentageModel());

            numberRecognizer = null;
        }

        private void AddNumberWithUnitModels(string culture)
        {
            var numberWithUnitRecognizer = new NumberWithUnitRecognizer(culture);
            _models.Add(numberWithUnitRecognizer.GetAgeModel());
            _models.Add(numberWithUnitRecognizer.GetCurrencyModel());
            _models.Add(numberWithUnitRecognizer.GetDimensionModel());
            _models.Add(numberWithUnitRecognizer.GetTemperatureModel());

            numberWithUnitRecognizer = null;
        }

        private void AddSequenceModels(string culture)
        {
            var sequenceRecognizer = new SequenceRecognizer(culture);
            _models.Add(sequenceRecognizer.GetEmailModel());
            _models.Add(sequenceRecognizer.GetGUIDModel());
            _models.Add(sequenceRecognizer.GetHashtagModel());
            _models.Add(sequenceRecognizer.GetIpAddressModel());
            _models.Add(sequenceRecognizer.GetMentionModel());
            _models.Add(sequenceRecognizer.GetPhoneNumberModel());
            _models.Add(sequenceRecognizer.GetQuotedTextModel());

            sequenceRecognizer = null;
        }
        public void RunTest(string test)
        {
            foreach (var model in _models)
                model.Parse(test);
        }
    }
}
