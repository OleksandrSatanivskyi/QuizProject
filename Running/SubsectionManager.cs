using QuizProject.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    public class SubsectionManager: CommandManager
    {
        public SubsectionManager(List<Section> Sections, List<Subsection> Subsections, List<Quiz> Quizzes)
        {
            this.Sections = Sections;
            this.Subsections = Subsections;
            this.Quizzes = Quizzes;
            IniCommandsInfo();
        }
        public void CreateSubsection()
        {
            Section s = null;
            bool checkSection = false;
            bool checkSubsection = true;
            Console.WriteLine("Введіть ім'я підрозділу");
            string subsectionName = Console.ReadLine();
            Console.WriteLine("Введіть ім'я розділу, до якого належить підрозділ");
            string sectionName = Console.ReadLine();
            foreach (var section in Sections)
            {
                if (section.Name == sectionName)
                {
                    s = section;
                    checkSection = true;
                    foreach (var subsection in Subsections)
                    {
                        if (subsection.Name == subsectionName)
                            checkSubsection = false;
                    }
                }
            }
            if (checkSection == true && checkSubsection == true)
            {
                Subsections.Add(new Subsection(subsectionName, s));
                Console.WriteLine("Підрозділ був успішно створений");
            }
            else
                Console.WriteLine("Помилка");
            Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
            Console.ReadKey(true);
        }
        public void DeleteSubsection()
        {
            Console.WriteLine("Введіть ім'я розділу, до якого належить підрозділ");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть ім'я підрозділу");
            string subsectionName = Console.ReadLine();
            var subsection = Subsections.SingleOrDefault(s => s.Name == subsectionName && s.Section == section);

            if (subsection == null)
                Console.WriteLine("Помилка");
            else
            {
                Subsections.Remove(subsection);
                Console.WriteLine("Підрозділ був успішно видалений");
            }
        }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
            new CommandInfo("назад", null, AllwaysDisplay),
            new CommandInfo("створити підрозділ", CreateSubsection, IfSectionsNotEmpty),
            new CommandInfo("видалити підрозділ", DeleteSubsection, IfSubsectionsNotEmpty),
            new CommandInfo("переіменувати підрозділ", RenameSubsection, IfSubsectionsNotEmpty),
            new CommandInfo("змінити розділ", ChangeSection, IfSubsectionsNotEmpty)
            };
        }

        private void ChangeSection()
        {
            bool check = true;
            try
            {
                Console.WriteLine("Введіть ім'я розділу");
                string sectionName = Console.ReadLine();
                Section section = Sections.Where(e => e.Name == sectionName).First();
                if (!Sections.Contains(section))
                    check = false;
                Console.WriteLine("Введіть ім'я підрозділу");
                string subsectionName = Console.ReadLine();
                Subsection subsection = Subsections.Where(e => e.Name == subsectionName).First();
                if (!Subsections.Contains(subsection))
                    check = false;
                Console.WriteLine("Введіть ім'я нового розділу");
                string newSectionName = Console.ReadLine();
                Section newSection = Sections.Where(e => e.Name == newSectionName).First();
                if (!Sections.Contains(newSection))
                    check = false;
                if (check == false)
                {
                    Console.WriteLine("Помилка");
                }
                else
                {
                    subsection.ChangeSection(newSection);
                    Console.WriteLine("Розділ було змінено");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка");
            }
            Console.ReadKey(true);
        }

        private void RenameSubsection()
        {
            bool check = true;
            try
            {
                Console.WriteLine("Введіть ім'я розділу");
                string sectionName = Console.ReadLine();
                Section section = Sections.Where(e => e.Name == sectionName).First();
                Console.WriteLine("Введіть ім'я підрозділу");
                string subsectionName = Console.ReadLine();
                Subsection subsection = Subsections.Where(e => e.Name == subsectionName).First();
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
                    subsection.Rename(newName);
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
        {
            Console.Clear();
        }
    }
}
