using System;
using System.Collections.Generic;

namespace QuizProject
{
    [Serializable]
    public struct Task
    {
        public string Name { get; private set; }
        public int Id;
        public string Question { get; private set; }
        public List<string> AnswerOptions { get; private set; }
        public string CorrectAnswer { get; private set; }
        private static int Count = 0;
        public Task(string Name, string Question,string CorrectAnswer,params string[] Options) 
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Wrong name!");
            Count++;
            Id = Count;
            this.Name = Name;
            this.Question = Question;
            this.CorrectAnswer= CorrectAnswer;
            this.AnswerOptions = new List<string>();
            this.AnswerOptions.AddRange(Options);
            Random rand = new Random();
            for (int i = AnswerOptions.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                string tmp = AnswerOptions[j];
                AnswerOptions[j] = AnswerOptions[i];
                AnswerOptions[i] = tmp;
            }
        }
        public void EditOptions(string answer, params string[] Options) 
        {
            CorrectAnswer=answer;
            AnswerOptions = new List<string>();
            AnswerOptions.AddRange(Options);
        }
        public override string ToString()
            => Name;
    }
}
