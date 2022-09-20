using QuizProject.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizProject
{
    public class QuizManager : CommandManager, IObjectManager<Quiz>
    {

        public QuizManager(List<Section> Sections, List<Quiz> Quizzes)
        {
            this.Sections = Sections;
            this.Quizzes = Quizzes;
            IniCommandsInfo();
        }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("назад", null, AllwaysDisplay),
                new CommandInfo("видалити вікторину", DeleteObject, IfQuizzesNotEmpty),
                new CommandInfo("створити вікторину", CreateObject, IfSectionsNotEmpty),
                new CommandInfo("переiменувати вікторину", RenameObject, IfSectionsNotEmpty),
                new CommandInfo("змінити розділ вікторини", ChangeSection, IfSectionsNotEmpty),
                new CommandInfo("додати запитання", CreateTask, IfQuizzesNotEmpty),
                new CommandInfo("видалити запитання", DeleteTask, IfQuizzesNotEmpty),
                new CommandInfo("редагувати варіанти відповіді", EditTask, IfQuizzesNotEmpty)
            };
        }

        public void CreateObject()
        {
            Console.WriteLine("Введіть ім'я вікторини");
            string quizName = Console.ReadLine();

            Console.WriteLine("Введіть ім'я розділу, до якого буде належати вікторина");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            if (section == null)
                Console.WriteLine("Помилка");
            else
            {
                Quizzes.Add(new Quiz(quizName, section));
                Console.WriteLine("Вікторина була успішно Створена");
            }
        }

        private Quiz GetObject()
        {
            Console.WriteLine("Введіть ім'я розділу, до якого належить вікторина");
            string sectionName = Console.ReadLine();
            var section = Sections.SingleOrDefault(s => s.Name == sectionName);

            Console.WriteLine("Введіть ім'я вікторини");
            string quizName = Console.ReadLine();
            var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
                                              && q.Section == section);
            return quiz;
        }

        Quiz IObjectManager<Quiz>.GetObject()
            => this.GetObject();

        public void RenameObject()
        {
            var quiz = this.GetObject();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                quiz.Name = Console.ReadLine();
                Console.WriteLine("Вікторина була успішно видалена");
            }
        }

        public void DeleteObject()
        {
            var quiz = this.GetObject();

            if (quiz == null)
                Console.WriteLine("Помилка");
            else
            {
                Quizzes.Remove(quiz);
                Console.WriteLine("Вікторина була успішно видалена");
            }
        }

        private void ChangeSection()
        {
            var quiz = GetObject();
            
            Console.WriteLine("Введіть ім'я нового розділу");
            string newSectionName = Console.ReadLine();
            Section newSection = Sections.SingleOrDefault(e => e.Name == newSectionName);
            if (newSection == null)
                Console.WriteLine("Помилка");
            else 
            {
                quiz.Section=newSection;
                Console.WriteLine("Розділ був успішно змінений");
            }
        }


        private void EditTask()
        {
            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            bool checkQuiz = false;
            foreach (var quiz in Quizzes)
            {
                if (quizName == quiz.Name)
                {
                    checkQuiz = true;
                    Console.WriteLine("Введіть назву запитання");
                    string taskName = Console.ReadLine();
                    foreach (var task in quiz.Tasks)
                    {
                        if (task.Name == taskName)
                        {
                            Console.WriteLine("Введіть правильну відповідь запитання");
                            string answer = Console.ReadLine();
                            Console.WriteLine("Вводьте варіанти відповіді з нового рядка\n(щоб припинити введіть \"exit\")");
                            List<string> variants = new List<string>();
                            string variant = "";
                            while (variant != "exit")
                            {
                                variant = Console.ReadLine();
                                variants.Add(variant);
                            }

                            task.EditOptions(answer, variants.ToArray());
                            break;
                        }
                    }

                }
            }
            if (checkQuiz == true)
            {
                Console.WriteLine("Запитання успішно відредаговане");
            }
            else
            {
                Console.WriteLine("Помилка");
            }
            Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
            Console.ReadKey(true);
        }

        private void DeleteTask()
        {
            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            bool checkQuestion = false;
            foreach (var quiz in Quizzes)
            {
                if (quizName == quiz.Name)
                {
                    Console.WriteLine("Введіть назву запитання");
                    string taskName = Console.ReadLine();
                    foreach (var task in quiz.Tasks)
                    {
                        if (task.Name == taskName)
                        {
                            checkQuestion = true;
                            quiz.Tasks.Remove(task);
                        }
                    }
                    break;
                }
            }
            if (checkQuestion == true)
            {
                Console.WriteLine("Запитання успішно видалене");
            }
            else
            {
                Console.WriteLine("Помилка");
            }
            Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
            Console.ReadKey(true);
        }

        private void CreateTask()
        {
            Console.WriteLine("Введіть назву вікторини");
            string quizName = Console.ReadLine();
            bool checkQuiz = false;
            foreach (var quiz in Quizzes)
            {
                if (quizName == quiz.Name)
                {
                    checkQuiz = true;
                    Console.WriteLine("Введіть назву запитання");
                    string taskName = Console.ReadLine();
                    Console.WriteLine("Введіть запитання");
                    string question = Console.ReadLine();
                    Console.WriteLine("Введіть правильну відповідь запитання");
                    string answer = Console.ReadLine();
                    Console.WriteLine("Вводьте варіанти відповіді з нового рядка\n(щоб припинити введіть \"exit\")");
                    List<string> variants = new List<string>();
                    string variant = "";
                    while (variant != "exit")
                    {
                        variant = Console.ReadLine();
                        variants.Add(variant);
                    }
                    Task task = new Task(taskName, question, answer, variants.ToArray());
                    quiz.AddTask(task);
                    break;
                }
            }
            if (checkQuiz == true)
            {
                Console.WriteLine("Запитання успішно створене");
            }
            else
            {
                Console.WriteLine("Помилка");
            }
            Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
            Console.ReadKey(true);
        }

        protected override void PrepareScreen()
        {
            Console.Clear();
        }
        protected override void AfterScreen()
        {
            Console.ReadKey();
        }


    }
}
/*public void CreateObject()
{
Console.WriteLine("Введіть ім\'я вікторини");
string quizName = Console.ReadLine();
Console.WriteLine("Введіть розділ");
string sectionName = Console.ReadLine();
Console.WriteLine("Введіть підрозділ");
string subsectionName = Console.ReadLine();
bool checkSection = false;
bool checkSubsection = false;
foreach (var section in Sections)
{
if (section.Name == sectionName)
{
   checkSection = true;
   foreach (var subsection in Subsections)
   {
       if (subsection.Name == subsectionName)
       {
           checkSubsection = true;
           if (checkSection == true && checkSubsection == true)
           {
               Quizzes.Add(new Quiz(quizName, subsection));
               Console.WriteLine("Вікторина успішно створена!");

           }
           break;
       }
   }
}
}
if (checkSection == false || checkSubsection == false)
{
Console.WriteLine("Помилка");
}
Console.WriteLine("Нажміть будь-яку клавішу щоб повернутись в меню");
Console.ReadKey(true);
}

public Quiz GetObject(string name)
{
throw new NotImplementedException();
}
private void RenameObject()
{
bool check = true;
try
{
Console.WriteLine("Введіть ім'я вікторини");
string quizName = Console.ReadLine();
Quiz quiz = Quizzes.Where(e => e.Name == quizName).First();
Console.WriteLine("Введіть нове ім'я");
string newName = Console.ReadLine();
foreach (var q in Quizzes)
{
   if (q.Name == newName)
   {
       check = false;
   }
}
if (check == true)
{
   quiz.Rename(newName);
   Console.WriteLine("Розділ був успішно переіменований");
}
else
   Console.WriteLine("Помилка");
}
catch (Exception ex) 
{ 
Console.WriteLine("Помилка");
}
Console.ReadKey(true);
}

private void DeleteObject()
{
Console.WriteLine("Введіть ім'я розділу, до якого належить вікторина");
string sectionName = Console.ReadLine();
var section = Sections.SingleOrDefault(s => s.Name == sectionName);

Console.WriteLine("Введіть ім'я підрозділу, до якого належить вікторина");
string subsectionName = Console.ReadLine();
var subsection = Subsections.SingleOrDefault(s => s.Name == subsectionName && s.Section == section);

Console.WriteLine("Введіть ім'я вікторини");
string quizName = Console.ReadLine();
var quiz = Quizzes.SingleOrDefault(q => q.Name == quizName
                             && q.Section == section
                             && q.Subsection == subsection);

if (quiz == null)
Console.WriteLine("Помилка");
else
{
Subsections.Remove(subsection);
Console.WriteLine("Вікторина була успішно видалена");
}
}*/
