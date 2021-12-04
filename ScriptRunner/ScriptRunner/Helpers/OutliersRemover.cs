using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Helpers
{
    public static class OutliersRemover
    {
        public static List<double> Remove(List<double> list)
        {
            list.Sort();

            int mid = list.Count / 2;

            var left = list.GetRange(0, mid);
            var Q1 = GetMedian(left);

            var right = list.GetRange(mid + 1, mid);
            var Q3 = GetMedian(right);

            var IQR = Q3 - Q1;
            var tmp = 1.5 * IQR;

            var result = list.Where(x => x >= Q1 - tmp && x <= Q3 + tmp).ToList();
            return result;
        }

        public static double GetMedian(List<double> list)
        {
            var mid = list.Count / 2;

            if (list.Count % 2 == 1)
                return list[mid];

            return (list[mid] + list[mid - 1]) / 2;
        }
    }
}
