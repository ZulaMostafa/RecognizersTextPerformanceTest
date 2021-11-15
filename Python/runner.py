import json
import constants
import os
import services.configs_reader as configs_reader
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
allTests = []
for culture in configs["cultures"]: 
    path = os.path.join(configs["rootPath"], f'{culture}.json')

    with open(path) as f:
        tests = f.read() 

    json_data = json.loads(tests)
    test = [o['Input'] for o in json_data]
    allTests.extend(test)
print(allTests)


# init logging service
logging_service = logging_service()

# register loggers [TODO: add types to configs file]
logging_service.add_logger(console_logger())
logging_service.add_logger(file_logger())