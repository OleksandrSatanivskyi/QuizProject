using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class GameCommands: Commands
    {
        public GameCommands(CommandManager manager) 
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", Exit, AllwaysDisplay),
                new CommandInfo("Пройти вікторину", TakeQuiz, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за кількістю пройдених вікторин", CountOfTakenQuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за однією вікториною", QuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика поточного користувача", CurrentUserStatistic, IfQuizzesNotEmpty),
            };
        }

        private void CurrentUserStatistic()
        {
            Console.WriteLine(CurrentManager.CurrentUser.Name + ":");
            foreach (var s in CurrentManager.CurrentUser.Statistics)
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
                foreach (var user in CurrentManager.Users)
                    if (user.Statistics.ContainsKey(quiz))
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
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            var quiz = CurrentManager.Quizzes?.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);
            return quiz;
        }

        private void CountOfTakenQuizStatistic()
        {
            var usersStatistic = new Dictionary<User, int>();
            var takenQuizzes = new List<Quiz>();

            foreach (var user in CurrentManager.Users)
            {
                foreach (var s in user.Statistics)
                    if (s.Value == s.Key.MaximumScores)
                        takenQuizzes.Add(s.Key);
                usersStatistic.Add(user, takenQuizzes.Count);
            }

            foreach (var s in usersStatistic)
                Console.WriteLine(s.Key + " - " + s.Value);
        }

        private void TakeQuiz()
        {
            var quiz = this.GetQuiz();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                int Count = 0;
                foreach (var task in quiz.Tasks)
                {
                    Console.Clear();
                    Console.WriteLine(task.Question);

                    var answerOptions = new List<string>();
                    answerOptions.AddRange(task.AnswerOptions);
                    answerOptions.Add(task.CorrectAnswer);
                    Random rand = new Random();
                    for (int i = answerOptions.Count - 1; i >= 1; i--)
                    {
                        int j = rand.Next(i + 1);
                        string tmp = answerOptions[j];
                        answerOptions[j] = answerOptions[i];
                        answerOptions[i] = tmp;
                    }

                    for (int i = 0; i < answerOptions.Count; i++)
                        Console.WriteLine("\t" + (i + 1) + " - " + answerOptions[i]);
                    Console.WriteLine("\t0 - вихід");
                    Console.Write("Ваша відповідь: ");
                    string key = Console.ReadLine();
                    if (int.Parse(key) - 1 >= 0 && int.Parse(key) <= answerOptions.Count)
                    {
                        if (int.Parse(key) == 0)
                            return;
                        if (answerOptions[int.Parse(key) - 1] == task.CorrectAnswer)
                        {
                            Count += task.Score;
                            Console.WriteLine($"Ваша відповідь правильна. Ви отримуєте +{task.Score} балів");
                        }
                        else
                            Console.WriteLine("Ваша відповідь не правильна");
                        Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
                        Console.ReadKey(true);
                    }
                }
                Console.WriteLine("Вікторина пройдена");
                Console.WriteLine($"Ваша кількість балів: {Count} з {quiz.MaximumScores}");

                CurrentManager.CurrentUser.Statistics.Add(quiz, Count);
            }
        }

        public override void Exit()
        {
            CurrentManager.Commands = new MainCommands(CurrentManager);
            CurrentManager.Run();
        }
    }
}
