using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class TaskCommands : Commands, IObjectCommands<Task>
    {
        public TaskCommands(CommandManager manager)
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", Exit, AllwaysDisplay),
                new CommandInfo("додати запитання", CreateObject, IfQuizzesNotEmpty),
                new CommandInfo("видалити запитання", DeleteObject, IfQuizzesNotEmpty),
                new CommandInfo("редагувати варіанти відповіді", EditAnswerOptions, IfQuizzesNotEmpty),
                new CommandInfo("змінити кількість балів за запитання", EditScore, IfQuizzesNotEmpty)
            };
        }

        public void CreateObject()
        {
            Console.WriteLine("Введіть назву запитання");
            string taskName = Console.ReadLine();

            Console.WriteLine("Введіть назву вікторини, до якої буде належати запитання");
            string quizName = Console.ReadLine();

            Console.WriteLine("Введіть назву розділу, до якого належить вікторина");
            string sectionName = Console.ReadLine();
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == sectionName);

            var quiz = CurrentManager.Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть запитання");
                string question = Console.ReadLine();

                Console.WriteLine("Введіть кількість варіантів відповідей на запитання");
                int countOfAnswerOptions =Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Введіть правильну відповідь");
                string correctAnswer = Console.ReadLine();

                Console.WriteLine($"Введіть {countOfAnswerOptions - 1} хибних варіантів відповіді");
                List<string> answerOptions = new List<string>();
                for (int i = 0; i < countOfAnswerOptions - 1; i++)
                    answerOptions.Add(Console.ReadLine());

                Console.WriteLine("Введіть кількість балів за запитання");
                int score = int.Parse(Console.ReadLine());

                quiz.AddTask(new Task(taskName, question, correctAnswer, answerOptions, score));
                Console.WriteLine("Запитання було успішно додане");
            }
        }

        public void DeleteObject()
        {
            Console.WriteLine("Введіть назву розділу, до якого належить вікторина, до якої належить запитання");
            string sectionName = Console.ReadLine();
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини, до якої належить запитання");
            string quizName = Console.ReadLine();
            var quiz = CurrentManager.Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);

            Console.WriteLine("Введіть назву запитання");
            string taskName = Console.ReadLine();
            var task = quiz.Tasks.SingleOrDefault(t => t.Name == taskName);

            if (task == null)
                Console.WriteLine("Помилка");
            else
            {
                quiz.Tasks.Remove(task);
                Console.WriteLine("Запитання було успішно видалене");
            }
        }

        private Task GetObject()
        {
            Console.WriteLine("Введіть назву розділу, до якого належить вікторина, до якої належить запитання");
            string sectionName = Console.ReadLine();
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини, до якої належить запитання");
            string quizName = Console.ReadLine();
            var quiz = CurrentManager.Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);

            Console.WriteLine("Введіть назву запитання");
            string taskName = Console.ReadLine();
            var task = quiz.Tasks?.SingleOrDefault(t => t.Name == taskName);

            return task;
        }

        Task IObjectCommands<Task>.GetObject()
            => this.GetObject();

        public void RenameObject()
        {
            var task = this.GetObject();

            if (task == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть нову назву для запитання");
                task.Name = Console.ReadLine();
                Console.WriteLine("Запитання було успішно переіменоване");
            }
        }

        private void EditScore()
        {
            var task = this.GetObject();

            if (task == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть кількість балів за запитання");
                int score = int.Parse(Console.ReadLine());

                task.Score = score;
                Console.WriteLine("Кількість балів успішно змінена");
            }
        }

        private void EditAnswerOptions()
        {
            var task = this.GetObject();

            if (task == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть кількість варіантів відповідей на запитання");
                int countOfAnswerOptions = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Введіть правильну відповідь");
                string correctAnswer = Console.ReadLine();

                Console.WriteLine($"Введіть {countOfAnswerOptions - 1} хибних варіантів відповіді");
                List<string> answerOptions = new List<string>();
                for (int i = 0; i < countOfAnswerOptions - 1; i++)
                    answerOptions.Add(Console.ReadLine());

                task.CorrectAnswer = correctAnswer;
                task.AnswerOptions = answerOptions;
                Console.WriteLine("Варіанти відповіді були успішно змінені");
            }

        }

        public override void Exit()
        {
            CurrentManager.Commands = new DataCommands(CurrentManager);
            CurrentManager.Run();
        }
    }
}
