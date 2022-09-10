using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    public static class Entering
    {

        public static int EnterInt32(string prompt)
        {
            while (true)
            {
                Console.Write($"\t{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                Console.WriteLine("\tПомилка введення цілого числа");
            }
        }

        public static int EnterInt32(string prompt, int min, int max = int.MaxValue)
        {
            while (true)
            {
                int value = EnterInt32(prompt);
                if (value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Очікується значення в діапазоні від {min} до {max}");
            }
        }

        public static string EnterString(string prompt)
        {
            Console.Write($"\t{prompt}: ");
            string s = Console.ReadLine();
            s = s.Trim();
            return s;
        }
    }
}
