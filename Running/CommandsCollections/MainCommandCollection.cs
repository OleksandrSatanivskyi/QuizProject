using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class MainCommandCollection : CommandCollection
    {
        public DataContext dataContext = new DataContext();
        public MainCommandCollection(CommandManager Manager) 
        {
            dataContext.Load();
            CurrentManager = Manager;
            CurrentManager.Users = dataContext.dataSet.Users;
            CurrentManager.Sections = dataContext.dataSet.Sections;
            CurrentManager.Quizzes = dataContext.dataSet.Quizzes;
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід", Exit, AllwaysDisplay),
                new CommandInfo("Вибрати/змінити користувача", SelectCurrentUser, AllwaysDisplay),
                new CommandInfo("Проходження вікторин", TakingQuizzes, IfUserIsLogined),
                new CommandInfo("Створити тестові дані", CreateTestingdata, IfDataContextNotEmpty),
                new CommandInfo("Дані як текст", DataAsText, IfUserIsLogined),
                new CommandInfo("Редагувати дані", EditData, IfCurrentUserIsAdmin),
                new CommandInfo("Зберегти зміни", Save, IfUserIsLogined, true),
            };
        }

        private bool IfDataContextNotEmpty()
           => !dataContext.dataSet.Sections.Any() || dataContext.dataSet.Sections == null;

        private bool IfDataContextEmpty()
            => dataContext.dataSet.Sections.Any();

        private void TakingQuizzes()
            => CurrentManager.Commands = new GameCommandCollection(CurrentManager);

        private void SelectCurrentUser()
        {
            CurrentManager.Users = dataContext.dataSet.Users;
            UserCreator userCreator = new UserCreator(CurrentManager.Users);
            CurrentManager.CurrentUser = userCreator.CreateUser();
            if (!CurrentManager.Users.Contains(CurrentManager.CurrentUser))
                dataContext.dataSet.Users.Add(CurrentManager.CurrentUser);
        }

        private void Save()
        {
            dataContext.Save();
            Console.WriteLine("Зміни збережено");
        }

        private void EditData()
            => CurrentManager.Commands = new DataCommandCollection(CurrentManager);

        private void DataAsText()
            => CurrentManager.Commands = new TextCommandCollection(CurrentManager);

        private void CreateTestingdata()
            => dataContext.CreateTestingData();

        public override void Exit()
            => CurrentManager.Commands = null;

    }
}
