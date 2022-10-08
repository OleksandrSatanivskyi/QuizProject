using QuizProject.Data;
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
            var quiz = this.GetQuiz();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                var statistic = new Dictionary<User, int>();
                foreach (var user in Users)
                    if(user.Statistics.ContainsKey(quiz))
                        statistic.Add(user, user.Statistics[quiz]);
                statistic = statistic.OrderByDescending(s => s.Value).ToDictionary(s => s.Key, s => s.Value);

                Console.WriteLine(quiz + ":");
                int count = 1;
                foreach (var s in statistic)
                {
                    Console.WriteLine("\t" + count + ": " + s.Key + " - " + s.Value);
                    count++;
                }
            }
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
            var takenQuizzes = new List<Quiz>();
            foreach (var s in CurrentUser.Statistics)
                if (s.Value == s.Key.MaximumScores)
                    takenQuizzes.Add(s.Key);

            takenQuizzes.OrderBy(q => q.Name);

            Console.WriteLine("Пройдених вікторин: " + takenQuizzes.Count);
            for (int i = 0; i < takenQuizzes.Count; i++)
                Console.WriteLine("\t" + (i + 1) + " - " + takenQuizzes[i]);
        }

        private void TakeQuiz()
        {
            throw new NotImplementedException();
        }

        protected override void PrepareScreen()
           => Console.Clear();
    }
}
