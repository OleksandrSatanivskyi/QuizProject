using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class SectionCommands : Commands, IObjectCommands<Section>
    {
        public SectionCommands(CommandManager manager)
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", Exit, AllwaysDisplay),
                 new CommandInfo("створити розділ", CreateObject, IfCurrentUserIsAdmin),
                new CommandInfo("видалити розділ", DeleteObject, IfSectionsNotEmpty),
                new CommandInfo("переіменувати розділ", RenameObject, IfSectionsNotEmpty)
            };
        }

        public void CreateObject()
        {
            Console.WriteLine("Введіть назву розділу");
            string Name = Console.ReadLine();
            var section = CurrentManager.Sections.SingleOrDefault(s => s.Name == Name);

            if (section != null)
                Console.WriteLine("Помилка");
            else
            {
                CurrentManager.Sections.Add(new Section(Name));
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
                CurrentManager.Sections.Remove(section);
                Console.WriteLine("Розділ був успішно видалений");
            }
        }

        private Section GetObject()
        {
            Console.WriteLine("Введіть назву розділу");
            string name = Console.ReadLine();
            var section = CurrentManager.Sections?.SingleOrDefault(s => s.Name == name);
            return section;
        }

        Section IObjectCommands<Section>.GetObject()
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

        public override void Exit()
        {
            CurrentManager.Commands = new DataCommands(CurrentManager);
            CurrentManager.Run();
        }
    }
}
