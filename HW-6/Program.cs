using HW6;
using System;
using System.IO;

namespace HW6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{"СОТРУДНИКИ",34}");

            Repository repository = new Repository();

            //попытка загрузить файл
            try
            {
                repository = repository.LoadFileInfo();
            }
            catch
            {
                FileCreate();   //если нет файла, необходимо автоматически создать
                repository = repository.LoadFileInfo();

                Console.WriteLine("Для начала работы создайте хотя бы одну запись.\n");

                DataCreate(ref repository); //просьба пользователя создать запись

            }

            byte i;
            do
            {
                Console.WriteLine("1. Хочу вывести данные.\n2. Хочу отредактировать данные.\n3. Выйти.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        if (repository.NullCheck())
                        {
                            byte k;
                            do
                            {
                                Console.WriteLine("Это пустой файл. Создадим запись?");
                                Console.WriteLine("1. Да.\n2. Нет.");
                                k = Convert.ToByte(Console.ReadLine());

                                switch (k)
                                {
                                    case 1:
                                        DataCreate(ref repository);
                                        k = 2;
                                        break;
                                    case 2:
                                        break;
                                    default:
                                        Console.WriteLine("Ошибка!");
                                        break;
                                }
                            } while (k != 2);
                        }
                        else
                        {
                            DataOutput(repository);
                        }
                        break;
                    case 2:
                        ManageData(ref repository);
                        break;
                    case 3:
                        repository.SaveFileInfo();
                        break;
                    default:
                        Console.WriteLine("Ошибка!");
                        break;
                }
            } while (i != 3);
        }

        static void DataOutput(Repository repository)
        {
            byte i;
            do
            {
                Console.WriteLine("1. Вывод списка.\n2. Поиск рабочего." +
                    "\n3. Отсортировать по дате создания.\n4. Поиск в диапазоне дат.\n5. Уйти назад.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        repository.FullDataOutput(repository.Workers);
                        break;
                    case 2:
                        repository.NoteOutput();
                        break;
                    case 3:
                        byte k;
                        do
                        {
                            Console.WriteLine("\nКак хотите вывести данные? В порядке возрастания или убывания?");
                            Console.WriteLine("1. В порядке возрастания.\n2. В порядке убывания.\n3. Уйти назад.");
                            k = Convert.ToByte(Console.ReadLine());

                            switch (k)
                            {
                                case 1:
                                    repository.DateCreationSort(true);
                                    break;
                                case 2:
                                    repository.DateCreationSort(false);
                                    break;
                                case 3:
                                    break;
                                default:
                                    Console.WriteLine("Ошибка!");
                                    break;
                            }
                        } while (k != 3);
                        break;
                    case 4:
                        repository.RangeDataOutput();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Ошибка!");
                        break;
                }
            } while (i != 5);
        }

        static void DataCreate(ref Repository repository) //создание записей
        {
            byte i = 1;
            do
            {
                switch (i)
                {
                    case 1:
                        repository.NoteCreate();

                        Console.WriteLine("\nХотите внести ещё одного сотрудника?\n1. Да.\n2. Нет");
                        i = Convert.ToByte(Console.ReadLine());
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Ошибка!");
                        break;
                }
            } while (i != 2);
        }

        static void FileCreate()
        {
            using (FileStream fs = File.Create(@"Workers.txt"))
            {
                Console.WriteLine("Файл создан.");
            }
        }

        static void ManageData(ref Repository repository)
        {
            byte i;
            do
            {
                Console.WriteLine("1. Создать запись.\n2. Редактировать запись." +
                    "\n3. Удалить запись.\n4. Назад.");
                i = Convert.ToByte(Console.ReadLine());

                switch (i)
                {
                    case 1:
                        DataCreate(ref repository);
                        break;
                    case 2:
                        repository.NoteChange();
                        break;
                    case 3:
                        repository.NoteDelete();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Ошибка!");
                        break;
                }
            } while (i != 4);
        }
    }
}
