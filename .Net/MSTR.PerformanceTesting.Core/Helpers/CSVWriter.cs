using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTR.PerformanceTesting.Core.Helpers
{
    public static class CSVWriter
    {
        public static void Write(List<List<string>> content, string path, string filename)
        {
            // construct final path
            var finalPath = Path.Combine(path, $"{filename}.csv");

            // consturct csv final content
            var finalContent = content.Select(row => string.Join(',', row)).ToArray();

            // create directory if doesn't existp
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            // save file
            File.WriteAllLines(finalPath, finalContent);
        }
    }
}
