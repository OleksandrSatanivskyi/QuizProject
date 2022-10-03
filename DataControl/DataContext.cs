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
        public List<Task> Tasks { get; private set; }
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
            Tasks = dataSet.Tasks;
        }
        public void CreateTestingData() 
        {
            CreateSections();
            CreateTasks();
            CreateQuizzes();
        }

        private void CreateTasks()
        {
            #region Logic
            Tasks.Add(new Task("Логіка 1", "Що вийде при логічному додаванні 1 та 0 ?",
                "1","0", "2","4"));
            Tasks.Add(new Task("Логіка 2", "Для якого з наведених чисел істинно висловлювання:(число < 100) І(число парне) ? ",
                "44", "123", "104", "55"));
            Tasks.Add(new Task("Логіка 3", "Для якого з наведених імен істинне висловлювання:НЕ(Перша буква голосна) І НЕ(Остання буква згодна) ? ",
                "Тетяна", "Олег", "Роман", "Ганна"));
            #endregion

            #region Physics
            Tasks.Add(new Task("Фізика 1", "Слово \"фізика\" походить від грецького φύσις",
                "Природа", "Наука", "Дослід", "Сила"));
            Tasks.Add(new Task("Фізика 2", "Як називають стан тіла під час падіння?",
                "Невагомість", "Політ", "Сила тяжіння", "Падіння"));
            Tasks.Add(new Task("Фізика 3", "Як називається найменша порція енергії випромінюваної світлом?",
                "Квант", "Джоуль", "Електрон"));
            #endregion

            #region History
            Tasks.Add(new Task("Історія 1", "Який навчальний заклад вважають найдавнішим у Східній Європі?",
                "Києво-Могилянська академія", "Київський Видавничо-поліграфічний інститут", "Національний медичний університет імені Богомольця", "Консерваторія імені Глінки"));
            Tasks.Add(new Task("Історія 2", "Хто є батьком української історії?",
                "Михайло Грушеський", "Сергій Прокоф'єв", "Борис Фатон"));
            #endregion
        }

        private void CreateQuizzes()
        {
            Quizzes.Add(new Quiz("Вікторина на логіку",Sections[0],Tasks[0], Tasks[1], Tasks[2]));
            Quizzes.Add(new Quiz("Вікторина на фізику", Sections[1], Tasks[3], Tasks[4], Tasks[5]));
            Quizzes.Add(new Quiz("Вікторина на знання історії", Sections[2], Tasks[6], Tasks[7]));
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
            Tasks = dataSet.Tasks;
            if (Users == null)
                Users = new List<User>();
            if (Sections == null)
                Sections = new List<Section>();
            if (Quizzes == null)
                Quizzes = new List<Quiz>();
            if (Tasks == null)
                Tasks = new List<Task>();
        }
    }
}
