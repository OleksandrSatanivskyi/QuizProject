using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuizProject.Running
{
    public abstract class CommandManager
    {
        public User CurrentUser { get; set; }
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
                if (commandInfo.Command == null 
                    || commandInfo.Display() == false)
                    return;
                commandInfo.Command();
                AfterScreen();
            }
        }

        protected bool IfUserIsLogined()
           => this.CurrentUser != null;

        protected bool IfCurrentUserIsAdmin()
            => IfUserIsLogined() && this.CurrentUser.IsAdmin;

        protected static bool AllwaysDisplay()
            => true;

        protected bool IfSectionsNotEmpty()  
            => Sections != null && Sections.Any(); 

        protected bool IfQuizzesNotEmpty()  
            => Quizzes != null && Quizzes.Any(); 

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
            int num = Entering.EnterInt32("Номер команди", 0, commandsInfo.Length - 1);
            return commandsInfo[num];
        }

    }
}
