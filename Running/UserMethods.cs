using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject
{
    internal static class UserMethods
    {
        public static User SelectUser(List<User> Users) 
        {
            if (Users == null)
            {
                Users = new List<User>();
                return CreateNewUser(Users);
            }
            Console.WriteLine("Введіть ім'я користувача");
            string userName = Console.ReadLine();
            var user = Users.SingleOrDefault(u => u.Name == userName);

            if (user == null)
            {
                Console.WriteLine("Користувача з таким іменем не знайдено\n"+
                                  "Створити нового користувача?\n"+
                                  "1 - так\n"+
                                  "0 - назад");
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.D1)
                        return CreateNewUser(Users);
                    if (key.Key == ConsoleKey.D0)
                        return null;
                    else
                        Console.WriteLine("Помилка");
                }
            }
            else
                return UserAuthorization(Users, userName);
        }

        private static User UserAuthorization(List<User> Users, string userName)
        {
            Console.WriteLine("Введіть пароль в форматі [1-9][1-9][1-9][1-9]");
            string password = Console.ReadLine();
            User user = Users.SingleOrDefault
                (u => u.Name == userName && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Пароль не співпадає\n" +
                                  "1 - спробувати ще раз\n" +
                                  "0 - вихід\n");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D0)
                    return null;
                else
                    return UserAuthorization(Users, userName);
            }
            else
            {
                Console.WriteLine("Ви успішно авторизовані");
                return user;
            }
        }

        private static User CreateNewUser(List<User> Users)
        {
            Console.WriteLine("Введіть ім'я");
            string username = Console.ReadLine();
            Console.WriteLine("Введіть дату народження в форматі DD.MM.YYYY");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());
            if (birthDate < DateTime.Parse("01.12.1910") || birthDate > DateTime.Now)
                throw new ArgumentException("Wrong birth date!");
            Console.WriteLine("Введіть пароль в форматі [1-9][1-9][1-9][1-9]");
            string password = Console.ReadLine();
            User user = new User(username, birthDate, password);
            Users.Add(user);
            return user;
        }
    }
}
