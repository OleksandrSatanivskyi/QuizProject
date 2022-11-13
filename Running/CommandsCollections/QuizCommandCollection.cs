using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class QuizCommandCollection : CommandCollection, IObjectCommandCollection<Quiz>
    {
        public QuizCommandCollection(CommandManager manager)
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", Exit, AllwaysDisplay),
                new CommandInfo("видалити вікторину", DeleteObject, IfQuizzesNotEmpty),
                new CommandInfo("створити вікторину", CreateObject, IfSectionsNotEmpty),
                new CommandInfo("переiменувати вікторину", RenameObject, IfSectionsNotEmpty),
                new CommandInfo("змінити розділ вікторини", ChangeSection, IfSectionsNotEmpty)

            };
        }

        public void CreateObject()
        {
            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();

            Console.WriteLine("Введіть назву розділу, до якого буде належати вікторина");
            string sectionName = Console.ReadLine();
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == sectionName);

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                CurrentManager.Quizzes.Add(new Quiz(quizName, section));
                Console.WriteLine("Вікторина була успішно Створена");
            }
        }

        private Quiz GetObject()
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

        Quiz IObjectCommandCollection<Quiz>.GetObject()
            => this.GetObject();

        public void RenameObject()
        {
            var quiz = this.GetObject();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть нову назву для вікторини");
                quiz.Name = Console.ReadLine();
                Console.WriteLine("Вікторина була успішно переіменована");
            }
        }

        public void DeleteObject()
        {
            var quiz = this.GetObject();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                CurrentManager.Quizzes.Remove(quiz);
                Console.WriteLine("Вікторина була успішно видалена");
            }
        }

        private void ChangeSection()
        {
            var quiz = GetObject();

            Console.WriteLine("Введіть назву нового розділу");
            string newSectionName = Console.ReadLine();
            Section newSection = CurrentManager.Sections.SingleOrDefault(e => e.Name == newSectionName);
            if (newSection == null)
                Console.WriteLine("Помилка");
            else
            {
                quiz.Section = newSection;
                Console.WriteLine("Розділ був успішно змінений");
            }
        }

        public override void Exit()
        {
            CurrentManager.Commands = new DataCommandCollection(CurrentManager);
            CurrentManager.Run();
        }
    }
}
