using System.Xml.Linq;
using System.Linq;

namespace task1
{
    public class Program
    {
        public static void Main()
        {
            const string PATH = @"..\..\..\task1Xml.xml";
            const string ELEMENT_NAME = "teacher";

            var xDoc = XDocument.Load(PATH);
            var xRoot = xDoc.Root;
            if (xDoc == null || xRoot == null)
            {
                return;
            }
            
            var teacherList = xRoot.Elements(ELEMENT_NAME).ToList();

            if (teacherList.Count >= 2)
            {
                // Изменение атрибута у второго элемента
                var attr = teacherList[1].Attribute("FullName");
                if (attr != null)
                {
                    attr.Value = "Петр";
                }

                // Удаление элемента
                teacherList[0].Remove();
            }

            // Добавление элемента
            xRoot.Add(new XElement(ELEMENT_NAME,
                new XAttribute("FullName", "Игорь"),
                new XAttribute("Subject", "Информатика"),
                new XAttribute("Audience", "195")));

            xDoc.Save(PATH);
        }
    }
}
