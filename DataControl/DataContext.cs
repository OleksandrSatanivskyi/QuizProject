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
        private DataSet dataSet = new DataSet();
        public List<User> Users { get; private set; }
        public List<Section> Sections { get; private set; }
        public List<Quiz> Quizzes { get; private set; }
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
            Sections = dataSet.Sections;
            Quizzes = dataSet.Quizzes;
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
            var quiz1 = new Quiz("Вікторина на логіку", Sections[0], task1, task2, task3);
            Quizzes.Add(quiz1);

            task1 = new Task("Фізика 1", "Слово \"фізика\" походить від грецького φύσις", "Природа",
                new List<string> { "Наука", "Дослід", "Сила" }, 1);
            task2 = new Task("Фізика 2", "Як називають стан тіла під час падіння?", "Невагомість",
                new List<string> { "Політ", "Сила тяжіння", "Падіння" }, 1);
            task3 = new Task("Фізика 3", "Як називається найменша порція енергії випромінюваної світлом?", "Квант",
                new List<string> { "Джоуль", "Електрон" }, 1);
            var quiz2 = new Quiz("Вікторина на фізику", Sections[1], task1, task2, task3);
            Quizzes.Add(quiz2);

            task1 = new Task("Історія 1", "Який навчальний заклад вважають найдавнішим у Східній Європі?", "Києво-Могилянська академія",
                new List<string> { "Київський Видавничо-поліграфічний інститут",
                    "Національний медичний університет імені Богомольця", "Консерваторія імені Глінки" }, 1);
            task2 = new Task("Історія 2", "Хто є батьком української історії?", "Михайло Грушеський",
                new List<string> { "Сергій Прокоф'єв", "Борис Фатон" }, 1);
            var quiz3 = new Quiz("Вікторина на знання історії", Sections[2], task1, task2);
            Quizzes.Add(quiz3);
        }

        private void CreateSections()
        {
            Sections.Add(new Section("Формальні науки"));
            Sections.Add(new Section("Природничі науки"));
            Sections.Add(new Section("Соціально-гуманітарні науки"));
        }
        public override string ToString()
        {
            return string.Concat("Дані програми:\n",
               Sections.ToLineList("  Розділи"),
               Quizzes.ToLineList("  Вікторини"));
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
            Users = dataSet.Users;
            Sections = dataSet.Sections;
            Quizzes = dataSet.Quizzes;
            if (Users == null)
                Users = new List<User>();
            if (Sections == null)
                Sections = new List<Section>();
            if (Quizzes == null)
                Quizzes = new List<Quiz>();
        }
    }
}
