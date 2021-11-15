import time
import os
import psutil

class performance_model:
    def __init__(self):
        self.total_ticks = 0
        self.total_memory = 0

    def measure(self, action_to_be_done):
        # get total memory before execution
        memory_before_execution = psutil.Process(os.getpid()).memory_info().rss

        # get total ticks before execution
        ticks_before_execution = time.perf_counter()

        # execute function
        action_to_be_done()

        # get total ticks after execution
        ticks_after_execution = time.perf_counter()

        # get total memory after execution
        memory_after_execution = psutil.Process(os.getpid()).memory_info().rss

        # add results
        self.total_ticks += ticks_after_execution - ticks_before_execution
        self.total_memory += memory_after_execution - memory_before_execution


    def get_results(self):
        return \
        "Total Ticks: " + str(self.total_ticks) + '\n' + \
        "Total Memory: " + str(self.total_memory) + '\n'


