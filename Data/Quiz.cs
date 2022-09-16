using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    [Serializable]
    public class Quiz
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public Section Section { get; private set; }
        public Subsection Subsection { get; private set; }
        public List<Task> Tasks { get; }
        private static int Count=0;
        public Quiz(string Name, Subsection Subsection, params Task[] Tasks) 
        {
            Count++;
            Id = Count;
            this.Name = Name;
            this.Subsection = Subsection;
            this.Section = Subsection.Section;
            this.Tasks=new List<Task>();
            this.Tasks.AddRange(Tasks);
        }
        public void AddTask(Task task) 
        {
         Tasks.Add(task);
        }
        public Task GetTask(string name) 
        {
            foreach (Task t in Tasks)
                if (t.Name == name)
                    return t;
            throw new Exception("Your task was not found");
        }
        public Task GetTask(int Id)
        {
            foreach (Task t in Tasks)
                if (t.Id == Id)
                    return t;
            throw new Exception("Your task was not found");
        }
        public override string ToString()
        {
            return $"{Name}\n\tId: {Id}" +
                $"\n\tРозділ: {Section.Name}" +
                $"\n\tПідрозділ: {Subsection.Name}";
        }

        public void Rename(string newName)
            => Name = newName;
        

        public void ChangeLocation(Section newSection, Subsection newSubsection)
        {
           Section = newSection;
           Subsection = newSubsection;
        }
    }
}
