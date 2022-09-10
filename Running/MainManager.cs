using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizProject.Running
{
    public class MainManager : CommandManager
    {
        DataContext dataContext = new DataContext();
        DataEditor dataEditor;
        TextEditor textEditor;
        public MainManager()
        {
            dataContext.Load();
            dataEditor = new DataEditor(dataContext);
            textEditor = new TextEditor(dataContext);
        }
        private bool IfDataContextNotEmpty() { return !dataContext.Sections.Any(); }
        private bool IfDataContextEmpty() { return dataContext.Sections.Any(); }
        protected override void IniCommandsInfo()
        {
            commandsInfo = new CommandInfo[] {
                new CommandInfo("Вихід", null, AllwaysDisplay),
                new CommandInfo("Створити тестові дані", CreateTestingdata, IfDataContextNotEmpty),
                new CommandInfo("Дані як текст", DataAsText, IfDataContextEmpty),
                new CommandInfo("Редагувати дані", EditData , AllwaysDisplay),
                new CommandInfo("Зберегти зміни", Save , AllwaysDisplay, true),
            };
        }

        private void Save()
        {
            dataContext.Save();
            Console.WriteLine("Зміни збережено");
            Console.ReadKey();
        }

        private void EditData()
        {
            dataEditor.Run();
        }
        private void DataAsText()
        {
            textEditor.Run();
            //Console.WriteLine(dataContext);
            //Console.ReadKey(true);
        }
        private void CreateTestingdata()
        {
            dataContext.CreateTestingData();
        }
        protected override void PrepareScreen()
        {
            Console.Clear();
        }
    }
}
