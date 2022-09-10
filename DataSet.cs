using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    [Serializable]
    public class DataSet
    {
        public readonly List<Section> Sections = new List<Section>();
        public readonly List<Subsection> Subsections = new List<Subsection>();
        public readonly List<Quiz> Quizzes = new List<Quiz>();
        public readonly List<Task> Tasks = new List<Task>();
    }
}
