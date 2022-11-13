using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running.CommandInfos
{
    internal class TextCommandCollection : CommandCollection
    {
        public TextCommandCollection(CommandManager manager)
        {
            CurrentManager = manager;
            commandsInfo = new CommandInfo[]
            {
                new CommandInfo("до головного меню", Exit, AllwaysDisplay),
                new CommandInfo("статистика", Statistic, IfSectionsNotEmpty, true),
                new CommandInfo("вивести дані про розділи", WriteSections, IfSectionsNotEmpty),
                new CommandInfo("вивести дані про вікторини", WriteQuizzes, IfQuizzesNotEmpty),
                new CommandInfo("сортувати вікторини за назвою", SortByName, IfMoreThenOneQuiz),
                new CommandInfo("сортувати вікторини за розділом", SortByParentName, IfMoreThenOneQuiz),
                new CommandInfo("відібрати вікторину за частиною назви...", FilterByNameFragment, IfMoreThenOneQuiz, true)
            };
        }
        private bool IfMoreThenOneQuiz()
           => IfQuizzesNotEmpty() && CurrentManager.Quizzes.Count > 1;

        private void FilterByNameFragment()
        {
            string substring = Entering.EnterString("Фрагмент назви");
            var collection = CurrentManager.Quizzes.Where(e => e.Name.IndexOf(substring,
                StringComparison.InvariantCultureIgnoreCase) >= 0);
            if (!collection.Any())
                Console.WriteLine("Вікторина не знайдена");
            else
                Console.WriteLine(collection.ToLineList(""));
            Console.ReadKey();
        }

        private void SortByParentName()
        {
            CurrentManager.Quizzes = CurrentManager.Quizzes.OrderBy(e => e.Section.Name).ToList();
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in CurrentManager.Quizzes)
            {
                Console.WriteLine("\t" + quiz.Name);
            }
        }

        private void SortByName()
        {
            CurrentManager.Quizzes = CurrentManager.Quizzes.OrderBy(e => e.Name).ToList();
            Console.WriteLine("Відсортовані вікторини:");
            foreach (var quiz in CurrentManager.Quizzes)
            {
                Console.WriteLine("\t" + quiz.Name);
            }
        }

        private void WriteQuizzes()
        {
            var result = new Tree("Вікторини");
            result.Style = new Style(Color.Purple_2);
            foreach (var s in CurrentManager.Sections)
            {
                var sectionNode = result.AddNode($"[paleturquoise1]{s.ToString()}[/]\n");
                var quizzes = CurrentManager.Quizzes.Where(q => q.Section == s);
                foreach (var q in quizzes)
                    sectionNode.AddNode($"[gold3_1]{q.ToString()}[/]\n");
            }
            AnsiConsole.Write(result);
        }

        private void WriteSections()
        {
            var sections = new Tree("Розділи");
            sections.Style = new Style(Color.Purple_2);
            foreach (var section in CurrentManager.Sections)
                sections.AddNode($"[paleturquoise1]{section.ToString()}[/]\n");
            AnsiConsole.Write(sections);
        }

        private void Statistic()
        {
            Console.WriteLine("Статистика:");
            Console.WriteLine($"\t{ "Розділів:",-10} { CurrentManager.Sections.Count}");
            Console.WriteLine($"\t{ "Вікторин:",-10} {CurrentManager.Quizzes.Count}");
        }

        public override void Exit()
        {
            CurrentManager.Commands = new MainCommandCollection(CurrentManager);
            CurrentManager.Run();
        }
    }
}
