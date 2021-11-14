using RecognizersTextPerformanceTest.Models;
using RecognizersTextPerformanceTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextPerformanceTest.Interfaces
{
    public interface ITestParser
    {
        public List<T> Parse<T>(string rawData);
    }
}
