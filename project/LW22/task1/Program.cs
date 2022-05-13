using System;
using System.Linq;
using System.Collections.Generic;

namespace task1
{
    public class Program
    {
        public static void Main()
        {
            var rand = new Random();
            var studentList = new List<Student>();
            var nameArray = new string[] { "Максим", "Олег", "Антон", "Тимофей", "Лука", "Игнат" };
            var subjectArray = new string[] { "Математика", "ООП", "Web", "Английский", "Информетика", "1С" };

            for (int i = 0; i < nameArray.Length; i++) {

                var subjectList = new List<Subject>();

                for (int j = 0; j < subjectArray.Length; j++)
                {
                    if (rand.Next(0, 2) == 0)
                    {
                        subjectList.Add(new Subject(subjectArray[j]));
                    }
                }

                studentList.Add(new Student(nameArray[i], subjectList));
            }



            foreach (var student in studentList)
            {
                Console.WriteLine(student.FullName);
                foreach (var subject in student.Subjects)
                {
                    Console.WriteLine('\t' + subject.Name);
                }
            }
        }
    }

    public class Student
    {
        public Student(string fullName, List<Subject> subjects)
        {
            FullName = fullName;
            Subjects = subjects;
        }

        public string FullName { get; set; }
        public List<Subject> Subjects { get; set; }

    }

    public struct Subject
    {
        public Subject(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = "";
    }
}
