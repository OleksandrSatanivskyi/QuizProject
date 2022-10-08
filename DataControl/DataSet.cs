using QuizProject.Data;
using System;
using System.Collections.Generic;

namespace QuizProject
{
    [Serializable]
    public class DataSet
    {
        public List<User> Users = new List<User>();
        public List<Section> Sections = new List<Section>();
        public List<Quiz> Quizzes = new List<Quiz>();
    }
}
