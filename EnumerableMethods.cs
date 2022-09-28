using System.Collections.Generic;

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
