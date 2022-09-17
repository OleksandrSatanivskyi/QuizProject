using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    public abstract class CommandManager
    {
        public List<Section> Sections { get; protected set; }
        public List<Quiz> Quizzes { get; protected set; }
        protected CommandInfo[] commandsInfo;

        protected abstract void IniCommandsInfo();

        public CommandManager()
        {
            IniCommandsInfo();
        }

        protected virtual void PrepareRunning() { }
        protected abstract void PrepareScreen();

        public void Run()
        {
            PrepareRunning();
            while (true)
            {
                PrepareScreen();
                ShowMenu();
                CommandInfo commandInfo = SelectCommandInfo();
                if (commandInfo.Command == null)
                    return;
                commandInfo.Command();
            }
        }
        protected static bool AllwaysDisplay() { return true; }
        protected bool IfSectionsNotEmpty() { return Sections.Any(); }
        protected bool IfQuizzesNotEmpty() { return Quizzes.Any(); }
        private void ShowMenu()
        {
            Console.WriteLine("Список команд меню:");
            for (int i = 0; i < commandsInfo.Length; i++)
            {
                if (commandsInfo[i].Display())
                {
                    Console.WriteLine($"{i} - {commandsInfo[i].Name}");
                }
            }
        }

        protected CommandInfo SelectCommandInfo()
        {
            int num = Entering.EnterInt32("Номер команди",0, commandsInfo.Length - 1);
            return commandsInfo[num];
        }

    }
}
