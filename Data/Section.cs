using System;

namespace QuizProject
{
    [Serializable]
    public class Section
    {
        public string Name { get; private set; }
        public int Id;
        private static int Count=0;

        public Section(string Name)
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Wrong name!");
            Count++;
            this.Name = Name;
            Id = Count;
        }

        public override string ToString()
        {
            return $"{Name}" +
                $"\n\tId: {Id}";
        }

        internal void Rename(string newName)
            => Name = newName;
    }
}
