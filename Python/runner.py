import services.tests_reader as tests_reader
from models.performance_model import performance_model
from services.text_recognizers_client import text_recognizers_client
import sys

# get culture and recognizer from args
culture = sys.argv[1]
recognizer = sys.argv[2]
tests_path = sys.argv[3]

performance_model = performance_model()

# read tests
culture_tests = tests_reader.read_tests(tests_path)

def run_test():

    # init recognizer client
    recognizer_client = text_recognizers_client(culture, recognizer)

    #run tests
    for test in culture_tests:
        recognizer_client.run_test(test)

performance_model.measure(run_test)

memory = str(performance_model.get_memory())
time = str(performance_model.get_ticks())

print(memory + " " + time)


