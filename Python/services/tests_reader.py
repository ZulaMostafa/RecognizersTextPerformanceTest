import json

def read_tests(path):
    with open(path) as f:
        tests = f.read() 

    json_data = json.loads(tests)
    return [o['Input'] for o in json_data]
    