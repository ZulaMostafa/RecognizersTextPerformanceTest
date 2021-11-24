import time
from guppy import hpy;

class performance_model:
    def __init__(self):
        self.total_ticks = 0
        self.total_memory = 0

    def calc(self, x):
        ret = 0
        for i in range(0, x.__len__()):
            ret += x[i].size
        return ret

    def measure(self, action_to_be_done):

        # get total ticks before execution
        ticks_before_execution = time.perf_counter()

        # create hpy
        memory_profile = hpy()

        # get total memory before execution
        memory_before_execution = memory_profile.heap().size

        # execute function
        action_to_be_done()

        # get total memory after execution
        memory_after_execution = memory_profile.heap().size

        # get total ticks after execution
        ticks_after_execution = time.perf_counter()

        # add results
        self.total_ticks = ticks_after_execution - ticks_before_execution
        self.total_memory = memory_after_execution - memory_before_execution

    def get_ticks(self):
        return self.total_ticks

    def get_memory(self):
        return self.total_memory


