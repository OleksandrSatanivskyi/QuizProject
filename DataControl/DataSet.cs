using System;
using System.Collections.Generic;

namespace QuizProject
{
    [Serializable]
    public class DataSet
    {
        public readonly List<Section> Sections = new List<Section>();
        public readonly List<Quiz> Quizzes = new List<Quiz>();
        public readonly List<Task> Tasks = new List<Task>();
    }
}
