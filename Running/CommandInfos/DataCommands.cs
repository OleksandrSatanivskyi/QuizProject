using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class DataCommands : Commands
    {
        public DataCommands(CommandManager manager)
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід в головне меню", Exit, AllwaysDisplay),
                new CommandInfo("Редагувати дані про розділи", EditSection, IfCurrentUserIsAdmin),
                new CommandInfo("Редагувати дані про вікторини", EditQuiz, IfCurrentUserIsAdmin),
                new CommandInfo("Редагувати дані про запитання", EditTask, IfCurrentUserIsAdmin),
            };
        }

        private void EditTask()
        {
            CurrentManager.Commands = new TaskCommands(CurrentManager);
            CurrentManager.Run();
        }

        private void EditSection()
        {
            CurrentManager.Commands = new SectionCommands(CurrentManager);
            CurrentManager.Run();
        }

        private void EditQuiz()
        {
            CurrentManager.Commands = new QuizCommands(CurrentManager);
            CurrentManager.Run();
        }

        public override void Exit()
        {
            CurrentManager.Commands = new MainCommands(CurrentManager);
            CurrentManager.Run();
        }
    }
}
