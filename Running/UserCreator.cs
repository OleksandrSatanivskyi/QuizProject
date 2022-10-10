using QuizProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    internal class UserCreator : IUserCreator
    {
        public List<User> Users { get; set; }

        public UserCreator(List<User> Users)
        {
            this.Users = Users;
        }

        public UserCreator() : this(new List<User>()) { }

        public User CreateUser()
        {
            if (Users == null || Users.Count <= 0)
            {
                Users = new List<User>();
                return CreateNewUser();
            }
            else
                return UserAutorization();
        }

        private User ExistingUserAutorization()
        {
            throw new NotImplementedException();
        }

        private User CreateNewUser(string userName = null)
        {
            if (userName == null)
            {
                Console.WriteLine("Введіть ім'я");
                userName = Console.ReadLine();
            }

            Console.WriteLine("Введіть дату народження в форматі DD.MM.YYYY");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());
            if (birthDate < DateTime.Parse("01.12.1910") || birthDate > DateTime.Now)
                throw new ArgumentException("Wrong birth date!");


            Console.WriteLine("Новий користувач буде адміном?");
            Console.WriteLine("1 - так\n"
                            + "0 - ні");
            var key = Console.ReadLine();
            if (key != "1"
                && key != "0")
                throw new ArgumentException("Uncorrect choice");
            bool isAdmin = false;
            if (key == "1")
                isAdmin = true;
            if (key == "0")
                isAdmin = false;


            Console.WriteLine("Введіть пароль в форматі [1-9][1-9][1-9][1-9]");
            string password = Console.ReadLine();


            User user = new User(username, birthDate, isAdmin, password);
            Users.Add(user);
            return user;
        }
    }
}
