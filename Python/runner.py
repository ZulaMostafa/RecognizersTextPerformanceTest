from Models.PerformanceModel import PerformanceModel
from Services.TextRecognizersClient import TextRecognizersClient
from recognizers_text import Culture

performane_model = PerformanceModel()
cultures = [ Culture.English ]
recognizers = [ 'NumberRecognizer']
text_recognizers_client = TextRecognizersClient(cultures, recognizers)
text_recognizers_client.run_test('twelve')

