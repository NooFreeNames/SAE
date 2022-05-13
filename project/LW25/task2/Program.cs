using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;


namespace task2
{
    public class Program
    {
        public static void Main()
        {
            const string PATH = @"..\..\..\task2Xml.xml";

            var xDoc = XDocument.Load(PATH);
            xDoc.Root?.RemoveAll();
            xDoc.Save(PATH);

            var students = new Student[] {
                new Student("Игорь", 17, "КС-228"),
                new Student("Олег", 18, "ПО-322"),
                new Student("Игнат", 16, "РТ-111")
            };

            var formatter = new XmlSerializer(typeof(Student[]));

            using (var fs = new FileStream(PATH, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, students);
            }
        }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Group { get; set; } = string.Empty;

        public Student()
        {
        }

        public Student(string name, int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }
    }
}
