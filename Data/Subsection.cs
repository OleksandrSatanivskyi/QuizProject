using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    [Serializable]
    public class Subsection
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public Section Section { get; private set; }
        private static int Count = 0;
        public Subsection(string Name, Section Section) 
        {
            Count++;
            this.Name = Name;
            this.Section = Section;
            Id = Count;
        }
        public Subsection():this("Subsection", new Section())
        {
            Count++;
            Id = Count;
        }
        public override string ToString()
        {
            return $"{Name}\n\tId: {Id}\n\tРозділ: {Section.Name}";
        }
        public void ChangeSection(Section section) 
        {
        Section = section;
        }
        public void Rename(string newName)
        {
           Name= newName;
        }
    }
}
