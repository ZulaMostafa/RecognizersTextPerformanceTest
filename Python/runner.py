import constants
import os
import services.configs_reader

# load configs file
configs = services.configs_reader.load_application_configs()
print(configs['rootPath'])
