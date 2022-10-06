using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject.Running
{
    internal class TaskManager : CommandManager, IObjectManager<Task>
    {
        public void CreateObject()
        {
            Console.WriteLine("Введіть назву запитання");
            string taskName = Console.ReadLine();

            Console.WriteLine("Введіть назву вікторини, до якої буде належати запитання");
            string quizName = Console.ReadLine();

            Console.WriteLine("Введіть назву розділу, до якого належить вікторина");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);

            if (quiz == null)
                Console.WriteLine("Помилка");
            else 
            {
                Console.WriteLine("Введіть запитання");
                string question = Console.ReadLine();

                Console.WriteLine("Введіть правильну відповідь");
                string correctAnswer = Console.ReadLine();

                Console.WriteLine("Введіть 3 хибних варіанти відповіді з нового рядка");
                List<string> answerOptions = new List<string>();
                for (int i = 0; i < 3; i++)
                    answerOptions.Add(Console.ReadLine());

                quiz.AddTask(new Task(taskName, question, correctAnswer, answerOptions));
                Console.WriteLine("Запитання було успішно додане");
            }
        }

        public void DeleteObject()
        {
            Console.WriteLine("Введіть назву розділу, до якого належить вікторина, до якої належить запитання");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини, до якої належить запитання");
            string quizName = Console.ReadLine();
            var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
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
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини, до якої належить запитання");
            string quizName = Console.ReadLine();
            var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);

            Console.WriteLine("Введіть назву запитання");
            string taskName = Console.ReadLine();
            var task = quiz.Tasks.SingleOrDefault(t => t.Name == taskName);

            return task;
        }

        Task IObjectManager<Task>.GetObject()
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

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", null, AllwaysDisplay),
                new CommandInfo("додати запитання", CreateObject, IfQuizzesNotEmpty),
                new CommandInfo("видалити запитання", DeleteObject, IfQuizzesNotEmpty),
                new CommandInfo("редагувати варіанти відповіді", EditAnswerOptions, IfQuizzesNotEmpty)
            };
        }

        private void EditAnswerOptions()
        {
            throw new NotImplementedException();
        }

        protected override void PrepareScreen()
            => Console.Clear();

        protected override void AfterScreen()
            => Console.ReadKey();
    }
}
