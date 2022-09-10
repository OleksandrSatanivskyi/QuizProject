﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    public class TextEditor : CommandManager
    {
        public TextEditor(DataContext dataContext) 
        {
            Sections=dataContext.Sections;
            Subsections=dataContext.Subsections;
            Quizzes=dataContext.Quizzes;
        }
        private bool IfMoreThenOneQuiz() { return Quizzes.Count > 1; }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[]
            {
                new CommandInfo("до головного меню", null, AllwaysDisplay),
                new CommandInfo("статистика", Statistic, IfSectionsNotEmpty, true),
                new CommandInfo("вивести дані про розділи", WriteSections, IfSectionsNotEmpty),
                new CommandInfo("вивести дані про підрозділи", WriteSubsections, IfSubsectionsNotEmpty),
                new CommandInfo("вивести дані про вікторини", WriteQuizzes, IfQuizzesNotEmpty),
                new CommandInfo("сортувати вікторини за назвою", SortByName, IfMoreThenOneQuiz),
                new CommandInfo("сортувати вікторини за розділом і підрозділом", SortByParentName, IfMoreThenOneQuiz),
                new CommandInfo("відібрати вікторину за початком назви...", FilterByStartName, IfMoreThenOneQuiz, true),
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
        private void FilterByStartName()
        {
            string startName = Entering.EnterString("Початок назви");
            var collection = Quizzes.Where(e => e.Name.StartsWith(startName,
                StringComparison.InvariantCultureIgnoreCase));
            if (!collection.Any())
                Console.WriteLine("Вікторина не знайдена");
            else
                Console.WriteLine(collection.ToLineList(""));
            Console.ReadKey();
        }

        private void SortByParentName()
        {
            Quizzes = (List<Quiz>)Quizzes.OrderBy(e => e.Section).ThenBy(e=>e.Subsection);
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in Quizzes)
            {
                Console.WriteLine("\t"+quiz.Name);
            }
        }

        private void SortByName()
        {
            Quizzes=(List<Quiz>)Quizzes.OrderBy(e=>e.Name);
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in Quizzes)
            {
                Console.WriteLine("\t" + quiz.Name);
            }
        }

        private void WriteQuizzes()
        {
            Console.WriteLine(Quizzes.ToLineList<Quiz>("Вікторини", "\n "));
            Console.ReadKey();
        }

        private void WriteSubsections()
        {
            Console.WriteLine(Subsections.ToLineList<Subsection>("Підрозділи", "\n "));
            Console.ReadKey();
        }

        private void WriteSections()
        {
            Console.WriteLine(Sections.ToLineList<Section>("Розділи", "\n "));
            Console.ReadKey();
        }
        private void Statistic()
        {
            Console.WriteLine("Статистика:");
            Console.WriteLine($"\t{ "Розділів:", -10} {Sections.Count}");
            Console.WriteLine($"\t{ "Підрозділів:",-10} {Subsections.Count}");
            Console.WriteLine($"\t{ "Вікторин:",-10} {Quizzes.Count}");
            Console.ReadKey(true);
        }

        protected override void PrepareScreen()
        {
            Console.Clear();
        }
    }
}
