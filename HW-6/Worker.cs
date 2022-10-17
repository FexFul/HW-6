using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HW6
{
    struct Worker
    {
        private int id;

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        private string creationDate;
        public string CreationDate
        {
            get { return this.creationDate; }
            private set { this.creationDate = value; }
        }

        private string fullName;
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        private byte age;
        public byte Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        private byte height;
        public byte Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        private string birthDate;
        public string BirthDate
        {
            get { return this.birthDate; }
            set { this.birthDate = value; }
        }

        private string birthPlace;
        public string BirthPlace
        {
            get { return this.birthPlace; }
            set { this.birthPlace = value; }
        }

        public Worker(int id, string creationDate, string fullName, byte age, byte height, string birthDate, string birthPlace)
        {
            this.id = id;
            this.creationDate = creationDate;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.birthDate = birthDate;
            this.birthPlace = birthPlace;
        }

        public void GetInfo(bool fullList) //вывод записей на экран
        {
            if (!fullList)
            {
                Head();
            }

            Console.WriteLine($"{id,-4} {creationDate,-18} {fullName,-28} {age,-8} {height,-5} {birthDate,-14} {birthPlace,-15}");
        }

        public void Head()
        {
            Console.WriteLine($"\n{"ID",-4} {"Дата заметки",-18} {"ФИО",-28} " +
                   $"{"Возраст",-8} {"Рост",-5} {"Дата рождения",-14} {"Место рождения",-15}");
            for (int i = 0; i < 97; i++)
            {
                Console.Write("—");
            }
            Console.WriteLine();
        }

        //получить время записи
        public void SetCreationDate() 
        {
            string now = DateTime.Now.ToString("g", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            creationDate = now;
        }
    }

    struct Repository
    {
        public Worker[] Workers;

        public Repository(params Worker[] Args)
        {
            Workers = Args;
        }

        //Первая загрузка из файла в массив
        public Repository LoadFileInfo() 
        {
            using (StreamReader sr = new StreamReader(@"Workers.txt"))
            {
                string[] lines = File.ReadAllLines(@"Workers.txt");

                Worker[] Workers = new Worker[lines.Length];

                int i = 0;

                foreach (var line in lines)
                {
                    string[] subline = line.Split('#');

                    Workers[i] = new Worker(Convert.ToInt32(subline[0]), Convert.ToString(subline[1]), Convert.ToString(subline[2]),
                        Convert.ToByte(subline[3]), Convert.ToByte(subline[4]), Convert.ToString(subline[5]), Convert.ToString(subline[6]));

                    i++;
                }
                return new Repository(Workers);
            }
        }

        //выгрузить в файл все изменения
        public void SaveFileInfo() 
        {
            using (StreamWriter sw = new StreamWriter(@"Workers.txt"))
            {
                foreach (Worker Worker in Workers)
                {
                    sw.WriteLine($"{Worker.ID}#{Worker.CreationDate}#{Worker.FullName}#" +
                        $"{Worker.Age}#{Worker.Height}#{Worker.BirthDate}#{Worker.BirthPlace}");
                }
            }
        }

        public bool NullCheck()
        {
            if (Workers.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //вывести все записи
        public void FullDataOutput(Worker[] Workers) 
        {
            int i = 0;

            Workers[i].Head();
            foreach (Worker Worker in Workers)
            {
                Workers[i].GetInfo(true);
                i++;
            }
        }

        private int SetID()
        {
            if (Workers == null)
            {
                return 1;
            }
            else
            {
                int[] ids = new int[Workers.Length];

                int i = 0;

                foreach (var id in ids)
                {
                    ids[i] = Workers[i].ID;
                    i++;
                }

                Array.Sort(ids);

                if ((ids.Length == 0) || (ids[0] != 1))
                {
                    return 1;
                }

                for (i = 0; i < ids.Length - 1; i++)
                {
                    if (ids[i] != (ids[i + 1] - 1))
                    {
                        return (ids[i] + 1);
                    }
                }

                return (ids.Length + 1);
            }
        }

        private int SearchID(string action)
        {
            Console.Write($"Введите ID сотрудника, данные которого необходимо {action}: ");
            int index = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < Workers.Length; i++)
            {
                if (Workers[i].ID == index)
                {
                    return i;
                }
            }
            return -1;
        }

        public void NoteOutput()
        {
            int index = SearchID("показать");

            if (index == -1)
            {
                Console.WriteLine("Сотрудник с данным ID не найден.");
                return;
            }

            Workers[index].GetInfo(false);
        }

        public void NoteCreate()
        {
            Worker Worker = new Worker();

            Worker.ID = SetID();

            Worker.SetCreationDate();

            Console.Write("Введите ФИО: ");
            Worker.FullName = Console.ReadLine();

            Console.Write("Введите возраст: ");
            Worker.Age = Convert.ToByte(Console.ReadLine());

            Console.Write("Введите рост: ");
            Worker.Height = Convert.ToByte(Console.ReadLine());

            Console.Write("Введите дату рождения: ");
            Worker.BirthDate = Console.ReadLine();

            Console.Write("Введите место рождения: ");
            Worker.BirthPlace = Console.ReadLine();

            Console.Write("Введенная запись:\n");
            Worker.GetInfo(false);

            //в конец массива сотрудников вставить новое значение
            Worker[] newWorkers = new Worker[Workers.Length + 1];

            newWorkers[newWorkers.Length - 1] = Worker;

            for (int i = 0; i < Workers.Length; i++)
            {
                newWorkers[i] = Workers[i];
            }

            Workers = newWorkers;
        }

        public void NoteChange()
        {
            int index = SearchID("изменить");

            if (index == -1)
            {
                Console.WriteLine("Сотрудник с данным ID не найден.");
                return;
            }

            byte k = 0;
            do
            {
                Console.WriteLine("Выбранная запись:");
                Workers[index].GetInfo(false);

                Console.WriteLine("\nКакой параметр необходимо изменить?");
                Console.WriteLine("1. ФИО.\n2. Возраст.\n3. Рост.\n" +
                    "4. Дата рождения.\n5. Место рождения.\n6. Отмена.");
                k = Convert.ToByte(Console.ReadLine());

                switch (k)
                {
                    case 1:
                        Console.Write("Введите новое ФИО: ");
                        Workers[index].FullName = Console.ReadLine();
                        Workers[index].SetCreationDate();
                        break;
                    case 2:
                        Console.Write("Введите новый возраст: ");
                        Workers[index].Age = Convert.ToByte(Console.ReadLine());
                        Workers[index].SetCreationDate();
                        break;
                    case 3:
                        Console.Write("Введите новый рост: ");
                        Workers[index].Height = Convert.ToByte(Console.ReadLine());
                        Workers[index].SetCreationDate();
                        break;
                    case 4:
                        Console.Write("Введите новую дату рождения: ");
                        Workers[index].BirthDate = Console.ReadLine();
                        Workers[index].SetCreationDate();
                        break;
                    case 5:
                        Console.Write("Введите новое место рождения: ");
                        Workers[index].BirthPlace = Console.ReadLine();
                        Workers[index].SetCreationDate();
                        break;
                    case 6:

                        break;
                    default:
                        Console.WriteLine("Ошибка выбора.");
                        break;
                }
            } while (k != 6);
        }

        public void NoteDelete()
        {
            int index = SearchID("удалить");

            if (index == -1)
            {
                Console.WriteLine("Сотрудник с данным ID не найден.");
                return;
            }

            Console.WriteLine("Выбранная запись подлежит удалению:");
            Workers[index].GetInfo(false);

            byte k;
            do
            {
                Console.WriteLine("\nВы уверены, что хотите удалить запись?\n1. Удалить.\n2. Отмена.");
                k = Convert.ToByte(Console.ReadLine());

                switch (k)
                {
                    case 1:
                        Worker[] newWorkers = new Worker[Workers.Length - 1];

                        for (int i = 0; i < index; i++)
                        {
                            newWorkers[i] = Workers[i];
                        }

                        for (int i = index + 1; i < Workers.Length; i++)
                        {
                            newWorkers[i - 1] = Workers[i];
                        }

                        Workers = newWorkers;

                        Console.WriteLine("Запись удалена.");
                        return;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Ошибка выбора.");
                        break;
                }
            } while (k != 2);
        }

        public void DateCreationSort(bool ascending)
        {
            Worker[] sortedWorkers = new Worker[Workers.Length];
            Array.Copy(Workers, sortedWorkers, Workers.Length);

            //массив дат под ключи
            DateTime[] dates = new DateTime[sortedWorkers.Length];
            int i = 0;

            foreach (Worker Worker in sortedWorkers)
            {
                dates[i] = DateTime.Parse(sortedWorkers[i].CreationDate, System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
                i++;
            }

            Array.Sort(dates, sortedWorkers);

            if (!ascending)
            {
                Array.Reverse(sortedWorkers);
            }

            FullDataOutput(sortedWorkers);
        }

        public void RangeDataOutput()
        {
            Console.WriteLine("Введите начальную дату: ");
            DateTime date1 = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

            Console.WriteLine("Введите конечную дату: ");
            DateTime date2 = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

            bool empty = true;
            bool oneTime = false;

            foreach (Worker Worker in Workers)
            {
                DateTime date = DateTime.Parse(Worker.CreationDate, System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));

                if ((date >= date1) && (date <= date2))
                {
                    if (!oneTime)
                    {
                        Worker.Head();
                        oneTime = true;
                    }

                    Worker.GetInfo(true);
                    empty = false;
                }
            }

            if (empty)
            {
                Console.WriteLine("В заданном диапазоне нет записей");
            }
        }
    }
}