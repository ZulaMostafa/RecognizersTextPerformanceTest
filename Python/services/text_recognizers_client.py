from recognizers_text import Culture, ModelResult
from recognizers_choice import ChoiceRecognizer
from recognizers_date_time import DateTimeRecognizer 
from recognizers_number import NumberRecognizer
from recognizers_number_with_unit import NumberWithUnitRecognizer 
from recognizers_sequence import SequenceRecognizer 

class text_recognizers_client:
    def __init__(self, cultures, recognizers):
        self.models = []
        
        print(cultures)
        print(recognizers)

        for culture in cultures:
            if 'Choice' in recognizers:
                self.add_choice_models(culture)

            if 'DateTime' in recognizers:
                self.add_date_time_models(culture)

            if 'Number' in recognizers:
                self.add_number_recognizers_models(culture)
            
            if 'NumberWithUnit' in recognizers:
                self.add_number_with_unit_recognizers_models(culture)

            if 'Sequence' in recognizers:
                self.add_sequence_models(culture)
        
        

    def add_choice_models(self, culture):
        choice_recognizer = ChoiceRecognizer(culture)
        self.models.append(choice_recognizer.get_boolean_model())

    def add_date_time_models(self, culture):
        date_time_recognizer = DateTimeRecognizer(culture)
        self.models.append(date_time_recognizer.get_datetime_model())

    def add_number_recognizers_models(self, culture):
        print(culture)
        number_recognizer = NumberRecognizer(culture)
        self.models.append(number_recognizer.get_number_model())
        self.models.append(number_recognizer.get_ordinal_model())
        self.models.append(number_recognizer.get_percentage_model())

    def add_number_with_unit_recognizers_models(self, culture):
        number_with_unit_recognizer = NumberWithUnitRecognizer(culture)
        self.models.append(number_with_unit_recognizer.get_age_model())
        self.models.append(number_with_unit_recognizer.get_currency_model())
        self.models.append(number_with_unit_recognizer.get_dimension_model())
        self.models.append(number_with_unit_recognizer.get_temperature_model())

    def add_sequence_models(self, culture):
        sequence_recognizer = SequenceRecognizer(culture)
        self.models.append(sequence_recognizer.get_phone_number_model())
        self.models.append(sequence_recognizer.get_ip_address_model())
        self.models.append(sequence_recognizer.get_mention_model())
        self.models.append(sequence_recognizer.get_hashtag_model())
        self.models.append(sequence_recognizer.get_url_model())
        self.models.append(sequence_recognizer.get_guid_model())
        self.models.append(sequence_recognizer.get_email_model())

    def run_test(self, input):
        for model in self.models:
            print(model.parse(input))