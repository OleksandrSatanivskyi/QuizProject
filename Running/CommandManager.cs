using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuizProject.Running
{
    public abstract class CommandManager
    {
        public User CurrentUser { get; protected set; }
        public List<User> Users { get; protected set; }
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
        protected abstract void AfterScreen();

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
                AfterScreen();
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
