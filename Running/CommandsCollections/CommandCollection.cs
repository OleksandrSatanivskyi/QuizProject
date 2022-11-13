using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    public abstract class CommandCollection
    {
        public CommandManager CurrentManager { get; set; }
        public CommandInfo[] commandsInfo { get; set; }
        public abstract void Exit();

        protected bool IfUserIsLogined()
          => this.CurrentManager.CurrentUser != null;

        protected bool IfCurrentUserIsAdmin()
            => IfUserIsLogined() && this.CurrentManager.CurrentUser.IsAdmin;

        protected static bool AllwaysDisplay()
            => true;

        protected bool IfSectionsNotEmpty()
            => CurrentManager.Sections != null && CurrentManager.Sections.Any();

        protected bool IfQuizzesNotEmpty()
            => CurrentManager.Quizzes != null && CurrentManager.Quizzes.Any();
    }
}
