using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    [Serializable]
    public class Section
    {
        public string Name { get; private set; }
        public int Id;
        private static int Count=0;
        public Section():this("Section") 
        {
            Count++;
            Id = Count;
        }
        public Section(string Name)
        {
            Count++;
            this.Name = Name;
            Id = Count;
        }
        public override string ToString()
        {
            return $"{Name}\n\tId: {Id}";
        }

        internal void Rename(string newName)
        {
            Name = newName;
        }
    }
}
