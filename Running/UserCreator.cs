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
            throw new NotImplementedException();
        }
    }
}
