using System;
using System.Text.RegularExpressions;


namespace QuizProject.Data
{
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
        public string Password 
        {
            get => Password;
            set 
            {
                Regex regex = new Regex(@"[1-9][1-9][1-9][1-9]");
                if (!regex.IsMatch(value))
                    Password = value;
                else
                    throw new ArgumentException();
            }
        }

        public User(string Name, DateTime BirthDate, string Password) 
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Wrong name!");
            if (BirthDate > DateTime.Parse("01.12.1910") && BirthDate < DateTime.Now)
                throw new ArgumentException("Wrong birth date!");
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.Password = Password;
        }

        public override string ToString()
            => this.Name;
    }
}
