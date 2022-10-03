using QuizProject.Running;
using System;
using System.Text;

namespace QuizProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //добавити CurrentUser в усі менеджери
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            try
            {
                MainManager mainManager = new MainManager();
                mainManager.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
        }
    }
}
