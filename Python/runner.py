import constants
import os
from models.performance_model import performance_model
import services.configs_reader as configs_reader
from services.text_recognizers_client import text_recognizers_client

# load configs file
configs = configs_reader.load_application_configs()

# create text recognizers client
text_recognizers_client = text_recognizers_client(configs["cultures"], configs["recognizers"])

# create performance model
performance_model = performance_model()