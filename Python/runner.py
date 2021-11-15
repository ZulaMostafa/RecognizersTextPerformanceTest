import constants
import os
import services.configs_reader as configs_reader
from models.performance_model import performance_model
from services.text_recognizers_client import text_recognizers_client
from services.logging_service import logging_service
from models.Logger.console_logger import console_logger
from models.Logger.file_logger import file_logger

# load configs file
configs = configs_reader.load_application_configs()

# create text recognizers client
text_recognizers_client = text_recognizers_client(configs["cultures"], configs["recognizers"])

# create performance model
performance_model = performance_model()

# init logging service
logging_service = logging_service()

# register loggers [TODO: add types to configs file]
logging_service.add_logger(console_logger())
logging_service.add_logger(file_logger())

logging_service.log('test', 'double test')