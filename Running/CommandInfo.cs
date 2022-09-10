using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    public delegate void Command();
    public class CommandInfo
    {
        public string Name { get; private set; }
        public Command Command;
        public Func<bool> Display;
        public bool KeyPressWating;
        public CommandInfo(string Name, Command Command, Func<bool> Display, bool KeyPressWating = false)
        {
            this.Name = Name;
            this.Command = Command;
            this.Display = Display;
            this.KeyPressWating = KeyPressWating;
        }
    }
}
