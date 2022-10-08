using QuizProject.Data;
using QuizProject.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject
{
    public class SectionManager: CommandManager, IObjectManager<Section>
    {
        public SectionManager(List<Section> Sections, List<Quiz> Quizzes, User currentUser)
        {
            this.Sections = Sections;
            this.Quizzes = Quizzes;
            CurrentUser = currentUser;
            IniCommandsInfo();
        }

        public void CreateObject()
        {
            Console.WriteLine("Введіть назву розділу");
            string Name = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == Name);

            if (section != null)
                Console.WriteLine("Помилка");
            else
            {
                Sections.Add(new Section(Name));
                Console.WriteLine("Розділ був успішно створений");
            }
        }

        public void DeleteObject()
        {
            var section = this.GetObject();

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                Sections.Remove(section);
                Console.WriteLine("Розділ був успішно видалений");
            }
        }

        private Section GetObject()
        {
            Console.WriteLine("Введіть назву розділу");
            string name = Console.ReadLine();
            var section = Sections?.SingleOrDefault(s => s.Name == name);
            return section;
        }

        Section IObjectManager<Section>.GetObject()
        => this.GetObject();

        public void RenameObject()
        {
            var section = this.GetObject();

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                Console.WriteLine("Введіть нове назву для вікторини");
                section.Name = Console.ReadLine();
                Console.WriteLine("Вікторина була успішно переіменована");
            }
        }

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", null, AllwaysDisplay),
                 new CommandInfo("створити розділ", CreateObject, IfCurrentUserIsAdmin),
                new CommandInfo("видалити розділ", DeleteObject, IfSectionsNotEmpty),
                new CommandInfo("переіменувати розділ", RenameObject, IfSectionsNotEmpty)
            };
        }

        protected override void PrepareScreen()
        {
            Console.Clear();
        }
        protected override void AfterScreen()
        {
            Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
            Console.ReadKey();
        }

    }
}
/*public void DeleteSection()
        {
            Console.WriteLine("Введіть ім'я розділу");
            string name = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == name);

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                Sections.Remove(section);
                Console.WriteLine("Розділ був успішно видалений");
            }
        }

        public void CreateSection()
        {
            Console.WriteLine("Введіть ім'я розділу");
            string name = Console.ReadLine();
            Section section = new Section(name);
            bool check = true;
            if (Sections.Contains(section))
                check = false;
            if (check == false)
            {
                Console.WriteLine("Помилка");
            }
            else
            {
                Sections.Add(section);
                Console.WriteLine("Розділ був успішно створений");
            }
            Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
            Console.ReadKey(true);
        }
        
        private void RenameSection()
        {
            bool check = true;
            try
            {
                Console.WriteLine("Введіть ім'я розділу");
                string sectionName = Console.ReadLine();
                Section section = Sections.Where(e => e.Name == sectionName).First();
                    Console.WriteLine("Введіть нове ім'я");
                    string newName = Console.ReadLine();
                    foreach (var s in Sections)
                    {
                        if (s.Name == newName)
                        {
                            check = false;
                        }
                    }
                    if (check == true)
                    {
                        section.Rename(newName);
                        Console.WriteLine("Розділ був успішно переіменований");
                    }
                    else
                        Console.WriteLine("Помилка");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка");
            }
            Console.ReadKey();
        }

        protected override void PrepareScreen()
            => Console.Clear();*/