using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject.Running
{
    public class MainManager : CommandManager
    {
        private DataContext dataContext = new DataContext();
        private DataManager dataManager { get; set; }
        private TextManager textManager { get; set; }
        private GameManager gameManager { get; set; }
        public MainManager()
        {
            dataContext.Load();
        }

        private bool IfDataContextNotEmpty() 
            => !dataContext.dataSet.Sections.Any() || dataContext.dataSet.Sections == null; 

        private bool IfDataContextEmpty() 
            => dataContext.dataSet.Sections.Any(); 

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід", null, AllwaysDisplay),
                new CommandInfo("Вибрати/змінити користувача", SelectCurrentUser, AllwaysDisplay),
                new CommandInfo("Проходження вікторин", TakingQuizzes, IfUserIsLogined),
                new CommandInfo("Створити тестові дані", CreateTestingdata, IfDataContextNotEmpty),
                new CommandInfo("Дані як текст", DataAsText, IfUserIsLogined),
                new CommandInfo("Редагувати дані", EditData , IfCurrentUserIsAdmin),
                new CommandInfo("Зберегти зміни", Save , IfUserIsLogined, true),
            };
        }

        private void TakingQuizzes()
            => gameManager.Run();

        private void SelectCurrentUser()
        {
            Users = dataContext.dataSet.Users;
            UserCreator userCreator = new UserCreator(Users);
            CurrentUser = userCreator.CreateUser();
            if(!Users.Contains(CurrentUser))
                dataContext.dataSet.Users.Add(CurrentUser);
            dataManager = new DataManager(dataContext, CurrentUser);
            textManager = new TextManager(dataContext, CurrentUser);
            gameManager = new GameManager(dataContext, CurrentUser);
        }

        private void Save()
        {
            dataContext.Save();
            Console.WriteLine("Зміни збережено");
        }

        private void EditData()
            => dataManager.Run();

        private void DataAsText()
            => textManager.Run();

        private void CreateTestingdata()
            => dataContext.CreateTestingData();

        protected override void PrepareScreen() 
            => Console.Clear();

        protected override void AfterScreen()
        {
            Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
            Console.ReadKey();
        }
    }
}
