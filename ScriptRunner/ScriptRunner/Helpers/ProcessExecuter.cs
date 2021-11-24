using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Helpers
{
    public static class ProcessExecuter
    {
        public static string GetPythonOutput(string pythonPath, string filePath, string args = "")
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(pythonPath, string.Join(' ', filePath, args))
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            return output;
        }
    }
}
