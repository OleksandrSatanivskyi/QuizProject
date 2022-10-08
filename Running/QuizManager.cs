using QuizProject.Data;
using QuizProject.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject
{
    public class QuizManager : CommandManager, IObjectManager<Quiz>
    {

        public QuizManager(List<Section> Sections, List<Quiz> Quizzes, User currentUser)
        {
            this.Sections = Sections;
            this.Quizzes = Quizzes;
            CurrentUser = currentUser;
            IniCommandsInfo();
        }

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", null, AllwaysDisplay),
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
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                Quizzes.Add(new Quiz(quizName, section));
                Console.WriteLine("Вікторина була успішно Створена");
            }
        }

        private Quiz GetObject()
        {
            Console.WriteLine("Введіть назву розділу, до якого належить вікторина");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            var quiz = Quizzes?.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);
            return quiz;
        }

        Quiz IObjectManager<Quiz>.GetObject()
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
                Quizzes.Remove(quiz);
                Console.WriteLine("Вікторина була успішно видалена");
            }
        }

        private void ChangeSection()
        {
            var quiz = GetObject();
            
            Console.WriteLine("Введіть назву нового розділу");
            string newSectionName = Console.ReadLine();
            Section newSection = Sections.SingleOrDefault(e => e.Name == newSectionName);
            if (newSection == null)
                Console.WriteLine("Помилка");
            else 
            {
                quiz.Section = newSection;
                Console.WriteLine("Розділ був успішно змінений");
            }
        }
    
        protected override void PrepareScreen()
            => Console.Clear();
        
        protected override void AfterScreen()
        {
            Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
            Console.ReadKey();
        }


    }
}
