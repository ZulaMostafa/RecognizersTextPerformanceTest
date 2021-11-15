import os
import constants
import json

def load_application_configs():
    configs_file_path = os.path.join(constants.configs_file_directory, constants.configs_file_name)

    if os.path.exists(configs_file_path):
        with open(configs_file_path) as f:
            configs = f.read() 
        return json.loads(configs)

    raise Exception("configs file missing")