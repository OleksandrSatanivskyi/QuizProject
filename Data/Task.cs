using System;
using System.Collections.Generic;

namespace QuizProject
{
    [Serializable]
    public class Task
    {
        public string Name { get; set; }
        public int Id;
        public string Question { get; set; }
        public List<string> AnswerOptions { get; set; }
        public string CorrectAnswer { get; set; }
        private static int Count = 0;

        public Task(string Name, string Question, string CorrectAnswer, List<string> AnswerOptions)
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Wrong name!");
            Count++;
            Id = Count;
            this.Name = Name;
            this.Question = Question;
            this.CorrectAnswer = CorrectAnswer;
            this.AnswerOptions = AnswerOptions;
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
