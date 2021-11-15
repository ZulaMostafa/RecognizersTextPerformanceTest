class console_logger: 
    def log(self, operation_name, message):
        print('\n'.join([f'{operation_name}:', message]))