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
            #region Quiz1
            var quiz1 = new Quiz("Логіка", dataSet.Sections[0]);
            var currentTask = new Task("Логіка 1", "Що вийде при логічному додаванні 1 та 0 ?", "1",
                new List<string> { "0", "2", "4" }, 1);
            quiz1.AddTask(currentTask);
            currentTask = new Task("Логіка 2", "Для якого з наведених чисел істинно висловлювання:(число < 100) І(число парне) ? ",
                "44", new List<string> { "123", "104", "55" }, 1);
            quiz1.AddTask(currentTask);
            currentTask = new Task("Логіка 3", "Для якого з наведених імен істинне висловлювання:НЕ(Перша буква голосна) І НЕ(Остання буква згодна) ? ",
                "Тетяна", new List<string> {  "Олег", "Роман", "Ганна"}, 1);
            quiz1.AddTask(currentTask);

            dataSet.Quizzes.Add(quiz1);
            #endregion

            #region Quiz2
            var quiz2 = new Quiz("Фізика", dataSet.Sections[1]);
            currentTask = new Task("Запитання 1", "Зміна з часом положення тіла або частин тіла в просторі відносно інших тіл це", "Механічний рух",
                new List<string> { "Тіло відліку", "Система відліку", "Траєкторія руху" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 2", "Як називають стан тіла під час падіння?", "Невагомість",
                new List<string> { "Політ", "Сила тяжіння", "Падіння" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 3", "Як називається найменша порція енергії випромінюваної світлом?", "Квант",
                new List<string> { "Джоуль", "Електрон" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 4", "Тіло, відносно якого фіксується положення тіла, що рухається це", "Тіло відліку",
                new List<string> { "Матеріальна точка", "Траєкторія руху", "Система відліку" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 5", "Потяг, який прямує від однієї станції до іншої, перебуває у стані спокою відносно", "пасажира, що сидить у вагоні",
                new List<string> { "Землі", "світлофора", "спостерігача, який стоїть на пероні вокзалу" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 6", "Потяг, який прямує від однієї станції до іншої, перебуває у стані спокою відносно", "пасажира, що сидить у вагоні",
               new List<string> { "Землі", "світлофора", "спостерігача, який стоїть на пероні вокзалу" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 7", "Коли автомобіль можна вважати матеріальною точкою ? ", "Коли він їде з Києва до Дніпра.",
              new List<string> { "Коли він заїжджає у гараж", "Коли у нього сідають пасажири", "Коли його ремонтують на станції техобслуговування" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 8", "Який об'єкт не обов'язковий у системі відліку?", "Прилад для фіксації руху",
              new List<string> { "Тіло відліку", "Прилад для фіксації руху", "Система координат, пов'язана з тілом відліку" }, 1);
            quiz2.AddTask(currentTask);
            currentTask = new Task("Запитання 9", "Яке тіло відліку зручно обрати для того, щоб пояснити гостю нашого села як добратися до школи?", "Сільський магазин",
              new List<string> { "Сонце", "Пішохід", "Автомобіль", "Човен" }, 1);
            quiz2.AddTask(currentTask);

            dataSet.Quizzes.Add(quiz2);
            #endregion

            #region Quiz3
            var quiz3 = new Quiz("Історія", dataSet.Sections[2]);
            currentTask = new Task("Історія 1", "Який навчальний заклад вважають найдавнішим у Східній Європі?", "Києво-Могилянська академія",
                new List<string> { "Київський Видавничо-поліграфічний інститут",
                    "Національний медичний університет імені Богомольця", "Консерваторія імені Глінки" }, 1);
            quiz3.AddTask(currentTask);
            currentTask = new Task("Історія 2", "Хто є батьком української історії?", "Михайло Грушеський",
                new List<string> { "Сергій Прокоф'єв", "Борис Фатон" }, 1);
            quiz3.AddTask(currentTask);

            dataSet.Quizzes.Add(quiz3);
            #endregion
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
