using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject.Running
{
    public class MainManager : CommandManager
    {
        private DataContext dataContext = new DataContext();
        public List<User> Users { get; set; }
        private DataManager dataManager { get; set; }
        private TextManager textManager { get; set; }
        public MainManager()
        {
            dataContext.Load();
            dataManager = new DataManager(dataContext);
            textManager = new TextManager(dataContext);
        }
        private bool IfDataContextNotEmpty() { return !dataContext.Sections.Any(); }
        private bool IfDataContextEmpty() { return dataContext.Sections.Any(); }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід", null, AllwaysDisplay),
                new CommandInfo("Вибрати/змінити користувача", SelectCurrentUser, AllwaysDisplay),
                new CommandInfo("Створити тестові дані", CreateTestingdata, IfDataContextNotEmpty),
                new CommandInfo("Дані як текст", DataAsText, IfDataContextEmpty),
                new CommandInfo("Редагувати дані", EditData , AllwaysDisplay),
                new CommandInfo("Зберегти зміни", Save , AllwaysDisplay, true),
            };
        }

        private void SelectCurrentUser()
        {
            Users = dataContext.Users;
            CurrentUser = UserMethods.SelectUser(Users);
            if (Users == null)
                Users = new List<User>();
            if(!Users.Contains(CurrentUser))
                Users.Add(CurrentUser);
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
