using QuizProject.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject
{
    public class DataEditor : CommandManager
    {
        SubsectionEditor subsectionEditor;
        SectionEditor sectionEditor;
        QuizEditor quizEdutor;
        public DataEditor(DataContext dataContext) 
        {
            Sections=dataContext.Sections;
            Subsections=dataContext.Subsections;
            Quizzes=dataContext.Quizzes;
            subsectionEditor = new SubsectionEditor(Sections, Subsections, Quizzes);
            sectionEditor = new SectionEditor(Sections, Subsections, Quizzes);
            quizEdutor = new QuizEditor(Sections, Subsections, Quizzes);



        }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід в головне меню", null, AllwaysDisplay),
                new CommandInfo("Змінити розділ", EditSection, AllwaysDisplay),
                  new CommandInfo("Змінити підрозділ", EditSubsection, AllwaysDisplay),
                  new CommandInfo("Змінити вікторину", EditQuiz, AllwaysDisplay),
            };
        }

        private void EditSubsection()
        {
            subsectionEditor.Run();
        }

        private void EditSection()
        {
            sectionEditor.Run();
        }
        
        private void EditQuiz()
        {
            quizEdutor.Run();
        }
        

        protected override void PrepareScreen()
        {
            Console.Clear();
        }
    }
}
