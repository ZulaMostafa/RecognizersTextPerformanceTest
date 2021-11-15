import json
import constants
import os
import services.configs_reader as configs_reader
import services.tests_reader as tests_reader
from models.performance_model import performance_model
from services.text_recognizers_client import text_recognizers_client
from services.logging_service import logging_service
from models.Logger.console_logger import console_logger
from models.Logger.file_logger import file_logger
from operator import attrgetter

# load configs file
configs = configs_reader.load_application_configs()

# create text recognizers client
text_recognizers_client = text_recognizers_client(configs["cultures"], configs["recognizers"])

# create performance model
performance_model = performance_model()

# load tests [TODO: enhance loading tests for python]
all_Tests = []
for culture in configs["cultures"]: 
    path = os.path.join(configs["rootPath"], f'{culture}.json')
    culture_tests = tests_reader.read_tests(path)
    all_Tests.extend(culture_tests)

# run performance model
def runTests():
    for test in all_Tests:
        text_recognizers_client.run_test(test)

performance_model.measure(runTests)

# init logging service
logging_service = logging_service()

# register loggers [TODO: add types to configs file]
logging_service.add_logger(console_logger())
logging_service.add_logger(file_logger())

logging_service.log(configs['operationName'], performance_model.get_results())