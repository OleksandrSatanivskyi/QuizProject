using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class DataCommandCollection : CommandCollection
    {
        public DataCommandCollection(CommandManager manager)
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
            CurrentManager.Commands = new TaskCommandCollection(CurrentManager);
            CurrentManager.Run();
        }

        private void EditSection()
        {
            CurrentManager.Commands = new SectionCommandCollection(CurrentManager);
            CurrentManager.Run();
        }

        private void EditQuiz()
        {
            CurrentManager.Commands = new QuizCommandCollection(CurrentManager);
            CurrentManager.Run();
        }

        public override void Exit()
        {
            CurrentManager.Commands = new MainCommandCollection(CurrentManager);
            CurrentManager.Run();
        }
    }
}
