using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    internal class GameManager : CommandManager
    {

        protected override void AfterScreen()
            => Console.ReadKey();

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", null, AllwaysDisplay),
                new CommandInfo("Пройти вікторину", TakeQuiz, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за кількістю пройдених вікторин", CountOfTakenQuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за однією вікториною", QuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика поточного користувача", CurrentUserStatistic, IfQuizzesNotEmpty),
            };
        }

        private void CurrentUserStatistic()
        {
            Console.WriteLine(CurrentUser.Name + ":");
            foreach (var s in CurrentUser.Statistics)
                Console.WriteLine("\t" + s.Key + " - " + s.Value + "/" + s.Key.MaximumScores);
        }

        private void QuizStatistic()
        {
            //var quiz = this.GetQuiz();

            //if (quiz == null)
            //    Console.WriteLine("Помилка");
            //else
            //{
            //    foreach (var item in Users)
            //    {

            //    }
            //}
        }

        private Quiz GetQuiz() 
        {
            Console.WriteLine("Введіть назву розділу, до якого належить вікторина");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);
            return quiz;
        }

        private void CountOfTakenQuizStatistic()
        {
            throw new NotImplementedException();
        }

        private void TakeQuiz()
        {
            throw new NotImplementedException();
        }

        protected override void PrepareScreen()
           => Console.Clear();
    }
}
