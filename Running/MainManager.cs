﻿using QuizProject.Data;
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
            dataManager = new DataManager(dataContext, CurrentUser);
            textManager = new TextManager(dataContext, CurrentUser);
            gameManager = new GameManager(dataContext, CurrentUser);
        }

        private bool IfDataContextNotEmpty() 
            => !dataContext.Sections.Any() || dataContext.Sections == null; 

        private bool IfDataContextEmpty() 
            => dataContext.Sections.Any(); 

        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід", null, AllwaysDisplay),
                new CommandInfo("Вибрати/змінити користувача", SelectCurrentUser, AllwaysDisplay),
                new CommandInfo("Проходження вікторин", TakingQuizzes, IfUserIsLogined),
                new CommandInfo("Створити тестові дані", CreateTestingdata, IfUserIsLogined),
                new CommandInfo("Дані як текст", DataAsText, IfDataContextEmpty),
                new CommandInfo("Редагувати дані", EditData , IfCurrentUserIsAdmin),
                new CommandInfo("Зберегти зміни", Save , IfUserIsLogined, true),
            };
        }

        private void TakingQuizzes()
        {
            throw new NotImplementedException();
        }

        private void SelectCurrentUser()
        {
            Users = dataContext.Users;
            CurrentUser = UserMethods.SelectUser(Users);
            if(!Users.Contains(CurrentUser))
                Users.Add(CurrentUser);
            dataManager.CurrentUser = this.CurrentUser;
            textManager.CurrentUser = this.CurrentUser;
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
