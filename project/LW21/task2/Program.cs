using static System.Console;

namespace task2
{
    public class Program
    {
        public static void Main()
        {
            var studentArray = new Student[]
            {
                new Student("Максим", 18, "По-328"),
                new Student("Тимофей", 19, "ПО-328"),
                new Student("Олег", 17, "ПО-328"),
            };

            WriteLine("Min: " + studentArray.Min(student => student.Age));
            WriteLine("Max: " + studentArray.Max(student => student.Age));
            WriteLine("Average: " + studentArray.Average(student => student.Age));
            WriteLine("Sum: " + studentArray.Sum(student => student.Age));
        }
    }

    public struct Student
    {
        public Student(string name, int age, string group)
        {
            FullName = name;
            Age = age;
            Group = group;
        }

        public string FullName { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }

    }
}
