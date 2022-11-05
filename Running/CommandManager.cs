﻿using QuizProject.Data;
using QuizProject.Running.CommandInfos;
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuizProject.Running
{
    public abstract class CommandManager
    {
        public User CurrentUser { get; set; }
        public List<User> Users { get; set; }
        public List<Section> Sections { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public Commands Commands { get; set; }

        public CommandManager() {  }

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
                if (Commands == null)
                    return;
                if(commandInfo.Display() && Commands != null)
                    commandInfo.Command();
                AfterScreen();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("Список команд меню:");
            for (int i = 0; i < Commands.commandsInfo.Length; i++)
            {
                if (Commands.commandsInfo[i].Display())
                {
                    Console.WriteLine($"{i} - {Commands.commandsInfo[i].Name}");
                }
            }
        }

        protected CommandInfo SelectCommandInfo()
        {
            int num = Entering.EnterInt32("Номер команди", 0, Commands.commandsInfo.Length - 1);
            return Commands.commandsInfo[num];
        }

    }
}
