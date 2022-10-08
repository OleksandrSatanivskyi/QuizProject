using QuizProject.Data;
using QuizProject.Running;
using System;

namespace QuizProject
{
    public class DataManager : CommandManager
    {
        SectionManager sectionManager;
        QuizManager quizManager;
        TaskManager taskManager;

        public DataManager(DataContext dataContext, User currentUser) 
        {
            Users = dataContext.dataSet.Users;
            CurrentUser = CurrentUser;
            Sections = dataContext.dataSet.Sections;
            Quizzes = dataContext.dataSet.Quizzes;
            sectionManager = new SectionManager(Sections, Quizzes, CurrentUser);
            quizManager = new QuizManager(Sections, Quizzes, CurrentUser);
            taskManager = new TaskManager(Sections, Quizzes, CurrentUser);
        }

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід в головне меню", null, AllwaysDisplay),
                new CommandInfo("Змінити розділ", EditSection, IfCurrentUserIsAdmin),
                new CommandInfo("Змінити вікторину", EditQuiz, IfCurrentUserIsAdmin),
                new CommandInfo("Змінити запитання", EditTask, IfCurrentUserIsAdmin),
            };
        }

        private void EditTask()
            => taskManager.Run();

        private void EditSection()
            => sectionManager.Run();
        
        private void EditQuiz()
            => quizManager.Run();

        protected override void PrepareScreen()
            => Console.Clear();

        protected override void AfterScreen()
        {
            Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
            Console.ReadKey();
        }
    }
}
