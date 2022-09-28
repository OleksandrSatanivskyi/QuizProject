using QuizProject.Running;
using System;
using System.Text;

namespace QuizProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            MainManager mainManager = new MainManager();
  
            mainManager.Run();
        }
    }
}
