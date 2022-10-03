using System;
using System.Text.RegularExpressions;


namespace QuizProject.Data
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - BirthDate.Year;
                if (BirthDate > DateTime.Now.AddYears(-age))
                    age--;
                return age;
            }
        }
        public DateTime BirthDate { get; set; }
        public bool IsAdmin { get; set; }
        private string password;
        public string Password 
        {
            get => password;
            set 
            {
                Regex regex = new Regex(@"^[1-9][1-9][1-9][1-9]$");
                if (regex.IsMatch(value))
                    password = value;
                else
                    throw new ArgumentException("Wrong password!");
            }
        }

        public User(string Name, DateTime BirthDate, bool IsAdmin, string Password) 
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Wrong name!");
            if (BirthDate < DateTime.Parse("01.12.1910") || BirthDate > DateTime.Now)
                throw new ArgumentException("Wrong birth date!");
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.IsAdmin = IsAdmin;
            this.Password = Password;
        }

        public override string ToString()
            => this.Name;
    }
}
