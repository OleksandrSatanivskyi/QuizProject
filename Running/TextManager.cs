using QuizProject.Data;
using System;
using System.Linq;

namespace QuizProject.Running
{
    public class TextManager : CommandManager
    {
        public TextManager(DataContext dataContext, User currentUser) 
        {
            Users = dataContext.dataSet.Users;
            CurrentUser = currentUser;
            Sections = dataContext.dataSet.Sections;
            Quizzes = dataContext.dataSet.Quizzes;
        }
        private bool IfMoreThenOneQuiz()
            =>IfQuizzesNotEmpty() && Quizzes.Count > 1; 
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[]
            {
                new CommandInfo("до головного меню", null, AllwaysDisplay),
                new CommandInfo("статистика", Statistic, IfSectionsNotEmpty, true),
                new CommandInfo("вивести дані про розділи", WriteSections, IfSectionsNotEmpty),
                new CommandInfo("вивести дані про вікторини", WriteQuizzes, IfQuizzesNotEmpty),
                new CommandInfo("сортувати вікторини за назвою", SortByName, IfMoreThenOneQuiz),
                new CommandInfo("сортувати вікторини за розділом і підрозділом", SortByParentName, IfMoreThenOneQuiz),
                new CommandInfo("відібрати вікторину за частиною назви...", FilterByNameFragment, IfMoreThenOneQuiz, true)
            };
        }
        private void FilterByNameFragment()
        {
            string substring = Entering.EnterString("Фрагмент назви");
            var collection = Quizzes.Where(e => e.Name.IndexOf(substring,
                StringComparison.InvariantCultureIgnoreCase) >= 0);
            if (!collection.Any())
                Console.WriteLine("Вікторина не знайдена");
            else
                Console.WriteLine(collection.ToLineList(""));
            Console.ReadKey();
        }
       
        private void SortByParentName()
        {
            Quizzes = Quizzes.OrderBy(e => e.Section.Name).ToList();
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in Quizzes)
            {
                Console.WriteLine("\t"+quiz.Name);
            }
        }

        private void SortByName()
        {
            Quizzes = Quizzes.OrderBy(e => e.Name).ToList();
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in Quizzes)
            {
                Console.WriteLine("\t" + quiz.Name);
            }
        }

        private void WriteQuizzes()
        {
            foreach (var s in Sections)
            {
                Console.WriteLine(s);
                var quizzes = Quizzes.Where(q => q.Section == s);
                foreach (var q in quizzes)
                    Console.WriteLine("\t" + q);
            }
        }

        private void WriteSections()
        {
            Console.WriteLine(Sections.ToLineList<Section>("Розділи", "\n\t"));
        }
        private void Statistic()
        {
            Console.WriteLine("Статистика:");
            Console.WriteLine($"\t{ "Розділів:", -10} {Sections.Count}");
            Console.WriteLine($"\t{ "Вікторин:",-10} {Quizzes.Count}");
        }

        protected override void PrepareScreen()
        {
            Console.Clear();
        }
        protected override void AfterScreen()
        {
            Console.WriteLine("Нажміть будь-яку клавішу щоб продовжити");
            Console.ReadKey();
        }
    }
}
