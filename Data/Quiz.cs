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
        public string Name { get; set; }
        public int Id;
        public Section Section { get; private set; }
        public List<Task> Tasks { get; }
        private static int Count=0;

        public Quiz(string Name,Section Section, params Task[] Tasks) 
        {
            Count++;
            Id = Count;
            this.Name = Name;
            this.Section = Section;
            this.Tasks=new List<Task>();
            this.Tasks.AddRange(Tasks);
        }

        public Quiz(string Name, Section Section)
        {
            Count++;
            Id = Count;
            this.Name = Name;
            this.Section = Section;
            Tasks = new List<Task>();
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
                $"\n\tРозділ: {Section.Name}";
        }

        public void Rename(string newName)
            => Name = newName;

        public void ChangeLocation(Section newSection)
        {
           Section = newSection;
        }
    }
}
