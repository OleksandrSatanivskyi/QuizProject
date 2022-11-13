using QuizProject.Data;
using QuizProject.Running.CommandInfos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject.Running
{
    public class MainManager : CommandManager
    {
        
        public MainManager()
        {
            this.Commands = new MainCommandCollection(this);
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
