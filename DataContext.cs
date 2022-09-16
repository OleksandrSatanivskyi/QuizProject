using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuizProject
{
    [Serializable]
    public class DataContext
    {
        private DataSet dataSet=new DataSet();
        public List<Section> Sections { get; private set; }
        public List<Subsection> Subsections { get; private set; }
        public List<Quiz> Quizzes { get; private set; }
        public List<Task> Tasks { get; private set; }
        private string directoryName = "";
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
        public string FileName { get; set; } = "QuizProject.dat";
        public string FilePath=>Path.Combine(directoryName, FileName);
        public DataContext() 
        {
            Sections = dataSet.Sections;
            Subsections = dataSet.Subsections;
            Quizzes = dataSet.Quizzes;
            Tasks = dataSet.Tasks;
        }
        public void CreateTestingData() 
        {
            CreateSections();
            CreateSubsections();
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
            Quizzes.Add(new Quiz("Вікторина на логіку",Subsections[1],Tasks[0], Tasks[1], Tasks[2]));
            Quizzes.Add(new Quiz("Вікторина на фізику", Subsections[2], Tasks[3], Tasks[4], Tasks[5]));
            Quizzes.Add(new Quiz("Вікторина на знання історії", Subsections[4], Tasks[6], Tasks[7]));
        }

        private void CreateSubsections()
        {
            Subsections.Add(new Subsection("Математика", Sections[0]));
            Subsections.Add(new Subsection("Логіка", Sections[0]));
            Subsections.Add(new Subsection("Фізика", Sections[1]));
            Subsections.Add(new Subsection("Біологія", Sections[1]));
            Subsections.Add(new Subsection("Історія", Sections[2]));
            Subsections.Add(new Subsection("Правознавство", Sections[2]));
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
               Subsections.ToLineList("  Підрозділи"),
               Quizzes.ToLineList("  Вікторини")
               );
        }
        public void Save() 
        { 
            BinaryFormatter bFormatter= new BinaryFormatter();
            using (FileStream fstream=new FileStream(FileName, FileMode.Create,
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
            Sections = dataSet.Sections;
            Subsections = dataSet.Subsections;
            Quizzes = dataSet.Quizzes;
            Tasks = dataSet.Tasks;
        }
    }
}
