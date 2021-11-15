import constants
import os 

class file_logger:
    def log(self, operation_name, message):
        path = os.path.join(constants.logs_file_directory, f'{operation_name}.txt')
        f = open(path, "a")
        f.write(message)
        f.close()
