from models.Logger.console_logger import console_logger
from models.Logger.file_logger import file_logger

class logging_service:
    def __init__(self):
        self._loggers = []

    def add_logger(self, logger):
        self._loggers.append(logger)

    def log(self, operation_name, message):
        for logger in self._loggers:
            logger.log(operation_name, message)    