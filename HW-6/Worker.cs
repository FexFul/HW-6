using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HW6
{
    public struct Worker
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
}