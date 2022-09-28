using QuizProject.Running;
using System;

namespace QuizProject
{
    public class DataManager : CommandManager
    {
        SectionManager sectionManager;
        QuizManager quizManager;
        public DataManager(DataContext dataContext) 
        {
            Sections=dataContext.Sections;
            Quizzes=dataContext.Quizzes;
            sectionManager = new SectionManager(Sections, Quizzes);
            quizManager = new QuizManager(Sections, Quizzes);



        }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід в головне меню", null, AllwaysDisplay),
                new CommandInfo("Змінити розділ", EditSection, AllwaysDisplay),
                new CommandInfo("Змінити вікторину", EditQuiz, AllwaysDisplay),
            };
        }

        private void EditSection()
        {
            sectionManager.Run();
        }
        
        private void EditQuiz()
        {
            quizManager.Run();
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
