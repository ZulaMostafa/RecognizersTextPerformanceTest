using BenchmarkDotNet.Attributes;
using MSTR.PerformanceTesting.Core.Helpers;
using MSTR.PerformanceTesting.Definitions.Consts;
using MSTR.PerformanceTesting.Definitions.Enums;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Runners
{
    [MemoryDiagnoser]
    public class PythonRunner
    {
        public IEnumerable<string> Cultures
            => ConfigurationInitalizer.InitalizeCulturesList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.CulturesConfiguration));

        public IEnumerable<Recognizers> Recognizers
            => ConfigurationInitalizer.InitalizeRecognizersList(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.RecognizersConfiguration));

        public IEnumerable<int> Iterations
            => Enumerable.Range(1, int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.IterationsConfiguration)));

        [ParamsSource(nameof(Cultures))]
        public string culture { get; set; }

        [ParamsSource(nameof(Recognizers))]
        public Recognizers recognizer { get; set; }

        [ParamsSource(nameof(Iterations))]
        public int iteration;

        [Benchmark(Baseline = true)]
        public void RunTest()
        {
            //Runtime.PythonDLL = @"C:\\Users\\v-samostafa\\AppData\\Local\\Programs\\Python\\Python39\\python39.dll";

            var Directory = Path.Combine(Environment.GetEnvironmentVariable(EnvironmentVariablesStrings.MainDirectory), Directories.TestsDirectory);
            var tests = TestsReader.ReadTests(Directory, "English");
            using (Py.GIL())
            {
                var scope = Py.CreateScope();

                scope.Exec("from recognizers_text import Culture, ModelResult");

                /*scope.Exec("from recognizers_choice import ChoiceRecognizer");
                scope.Exec($"choice_recognizer = ChoiceRecognizer('{culture}')");
                scope.Exec("model = choice_recognizer.get_boolean_model()");*/


                scope.Exec("from recognizers_sequence import SequenceRecognizer");
                scope.Exec($"sequence_recognizer = SequenceRecognizer('{culture}')");
                scope.Exec("model = sequence_recognizer.get_phone_number_model()");

                scope.Exec($"f = open(r'{Directory}\\{culture}.json', encoding=\"utf8\")");

                scope.Exec("import json");
                scope.Exec("tests = f.read()");
                scope.Exec("json_data = json.loads(tests)");
                scope.Exec("inputs = [o['Input'] for o in json_data]");
                scope.Exec(@"for test in inputs:
                             model.parse(test)");

                scope.Dispose();
            }
        }
    }
}
