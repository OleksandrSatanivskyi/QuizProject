using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using QuizProject.Data;

namespace QuizProject
{
    [Serializable]
    public class DataContext
    {
        public DataSet dataSet { get; private set; }
        private string directoryName = @"C:\Users\" + Environment.UserName + @"\source\repos\QuizProject";
        public string DirectoryName
        {
            get { return directoryName; }
            set 
            {
                Regex regex = new Regex(@"\w+");
                if (!regex.IsMatch(directoryName))
                    throw new Exception("Directory format is not true");
                directoryName = Path.GetFullPath(value);
                if (!Directory.Exists(directoryName)) 
                Directory.CreateDirectory(directoryName);
            }
        }
        public const string FileName = "QuizProjectData.dat";
        public string FilePath => Path.Combine(directoryName, FileName);
        public DataContext() 
        {
            dataSet = new DataSet();
        }
        public void CreateTestingData() 
        {
            CreateSections();
            CreateQuizzes();
        }

        private void CreateQuizzes()
        {
            var task1 = new Task("Логіка 1", "Що вийде при логічному додаванні 1 та 0 ?", "1",
                new List<string> {"0", "2", "4" }, 1);
            var task2 = new Task("Логіка 2", "Для якого з наведених чисел істинно висловлювання:(число < 100) І(число парне) ? ",
                "44", new List<string> { "123", "104", "55" }, 1);
            var task3 = new Task("Логіка 3", "Для якого з наведених імен істинне висловлювання:НЕ(Перша буква голосна) І НЕ(Остання буква згодна) ? ",
                "Тетяна", new List<string> {  "Олег", "Роман", "Ганна"}, 1);
            var quiz1 = new Quiz("Вікторина на логіку", dataSet.Sections[0], task1, task2, task3);
            dataSet.Quizzes.Add(quiz1);

            task1 = new Task("Фізика 1", "Слово \"фізика\" походить від грецького φύσις", "Природа",
                new List<string> { "Наука", "Дослід", "Сила" }, 1);
            task2 = new Task("Фізика 2", "Як називають стан тіла під час падіння?", "Невагомість",
                new List<string> { "Політ", "Сила тяжіння", "Падіння" }, 1);
            task3 = new Task("Фізика 3", "Як називається найменша порція енергії випромінюваної світлом?", "Квант",
                new List<string> { "Джоуль", "Електрон" }, 1);
            var quiz2 = new Quiz("Вікторина на фізику", dataSet.Sections[1], task1, task2, task3);
            dataSet.Quizzes.Add(quiz2);

            task1 = new Task("Історія 1", "Який навчальний заклад вважають найдавнішим у Східній Європі?", "Києво-Могилянська академія",
                new List<string> { "Київський Видавничо-поліграфічний інститут",
                    "Національний медичний університет імені Богомольця", "Консерваторія імені Глінки" }, 1);
            task2 = new Task("Історія 2", "Хто є батьком української історії?", "Михайло Грушеський",
                new List<string> { "Сергій Прокоф'єв", "Борис Фатон" }, 1);
            var quiz3 = new Quiz("Вікторина на знання історії", dataSet.Sections[2], task1, task2);
            dataSet.Quizzes.Add(quiz3);
        }

        private void CreateSections()
        {
            dataSet.Sections.Add(new Section("Формальні науки"));
            dataSet.Sections.Add(new Section("Природничі науки"));
            dataSet.Sections.Add(new Section("Соціально-гуманітарні науки"));
        }
        public override string ToString()
        {
            return string.Concat("Дані програми:\n",
               dataSet.Sections.ToLineList("  Розділи"),
               dataSet.Quizzes.ToLineList("  Вікторини"));
        }

        public void Save() 
        { 
            BinaryFormatter bFormatter= new BinaryFormatter();
            using (FileStream fstream=new FileStream(FilePath, FileMode.OpenOrCreate,
                FileAccess.Write, FileShare.None)) 
            bFormatter.Serialize(fstream, dataSet);
        }

        public void Load() 
        {
            if (!File.Exists(FilePath))
                return;
            BinaryFormatter bFormatter= new BinaryFormatter();
            using (FileStream fstream = File.OpenRead(FilePath))
                dataSet=(DataSet)bFormatter.Deserialize(fstream);
            if (dataSet.Users == null)
                dataSet.Users = new List<User>();
            if (dataSet.Sections == null)
                dataSet.Sections = new List<Section>();
            if (dataSet.Quizzes == null)
                dataSet.Quizzes = new List<Quiz>();
        }
    }
}
