using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    public static class EnumerableMethods
    {

        public static string ToLineList<T>(this IEnumerable<T> collection, string prompt,
            string separator = "\n")
        {
            return string.Concat($"{prompt}:{separator}", string.Join(separator, collection), "\n");
        }
    }
}
