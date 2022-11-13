using QuizProject.Data;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class GameCommandCollection: CommandCollection
    {
        public GameCommandCollection(CommandManager manager) 
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", Exit, AllwaysDisplay),
                new CommandInfo("Пройти вікторину", TakeQuiz, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за кількістю ідеально пройдених вікторин", CountOfTakenQuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика за однією вікториною", QuizStatistic, IfQuizzesNotEmpty),
                new CommandInfo("Статистика поточного користувача", CurrentUserStatistic, IfQuizzesNotEmpty),
            };
        }

        private void CurrentUserStatistic()
        {
            Table statistic = new Table();
            statistic.Title($"[yellow1]{CurrentManager.CurrentUser.Name}[/]");
            statistic.AddColumns("Вікторина", "Бали", "Макс. бал");
            foreach (var s in CurrentManager.CurrentUser.Statistics)
               statistic.AddRow($"[lightskyblue3_1]{s.Key}[/]", $"[lightskyblue3_1]{s.Value}[/]", $"[lightskyblue3_1]{s.Key.MaximumScores}[/]");
            AnsiConsole.Write(statistic);
        }

        private void QuizStatistic()
        {
            WriteQuizzes();
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

                Table result = new Table();
                result.Title($"[yellow1]{quiz.Name}[/]");
                result.AddColumns("Місце", "Користувач", "Бал");
                int count = 1;
                foreach (var s in statistic)
                {
                    result.AddRow($"[lightskyblue3_1]{count}[/]", $"[lightskyblue3_1]{s.Key.Name}[/]", $"[lightskyblue3_1]{s.Value}[/]");
                    count++;
                }
                AnsiConsole.Write(result);
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
            usersStatistic = usersStatistic.OrderByDescending(s => s.Value).ToDictionary(s => s.Key, s => s.Value);

            Table result = new Table();
            result.AddColumns("Місце", "Користувач", "Кількість");
            int count = 1;
            foreach (var s in usersStatistic)
            {
                result.AddRow($"[lightskyblue3_1]{count}[/]", $"[lightskyblue3_1]{s.Key.Name}[/]", $"[lightskyblue3_1]{s.Value}[/]");
                count++;
            }
            AnsiConsole.Write(result);
        }

        private void TakeQuiz()
        {
            WriteQuizzes();

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

                    Table table = new Table();
                    table.Title("[yellow1]Варіанти відповіді[/]");
                    table.AddColumns("Номер", "Варіант");
                    for (int i = 0; i < answerOptions.Count; i++)
                        table.AddRow($"[white]{i + 1}[/]", $"[lightskyblue3_1]{answerOptions[i]}[/]");
                    table.AddRow($"[white]0[/]", $"[lightskyblue3_1]вихід[/]");
                    AnsiConsole.Write(table);
                    Console.Write("Ваша відповідь: ");
                    string key = Console.ReadLine();
                    if (int.Parse(key) >= 0 && int.Parse(key) <= answerOptions.Count)
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
            CurrentManager.Commands = new MainCommandCollection(CurrentManager);
            CurrentManager.Run();
        }

        private void WriteQuizzes()
        {
            var result = new Tree("Вікторини");
            result.Style = new Style(Color.Purple_2);
            foreach (var s in CurrentManager.Sections)
            {
                var sectionNode = result.AddNode($"[paleturquoise1]{s.ToString()}[/]\n");
                var quizzes = CurrentManager.Quizzes.Where(q => q.Section == s);
                foreach (var q in quizzes)
                    sectionNode.AddNode($"[gold3_1]{q.ToString()}[/]\n");
            }
            AnsiConsole.Write(result);
        }
    }
}
