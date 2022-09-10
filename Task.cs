using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    [Serializable]
    public struct Task
    {
        public string Name { get; private set; }
        public int Id;
        public string Question { get; private set; }
        public List<string> AnswerOptions;
        public string TrueAnswer { get; private set; }
        private static int Count = 0;
        public Task(string Name, string Question,string Answer,params string[] Options) 
        {
            Count++;
            Id = Count;
            this.Name = Name;
            this.Question = Question;
            this.TrueAnswer= Answer;
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
        TrueAnswer=answer;
            AnswerOptions = new List<string>();
            AnswerOptions.AddRange(Options);
        }
        public override string ToString()
        {
            string answers="";
            for (int i = 0; i < AnswerOptions.Count; i++)
            {
                answers +=AnswerOptions[i];
                answers+="\n";
            }
            return $"Question: {Question}\nAnswers: {answers}";
        }
    }
}
