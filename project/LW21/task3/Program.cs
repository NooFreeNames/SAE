using static System.Console;

namespace task2
{
    public class Program
    {
        public static void Main()
        {
            string[] countries = { "Испания", "Италия", "Индия", "Польша", "Португалия", "Бразилия" };

            foreach (var t in countries.TakeWhile(x => x.StartsWith('И')))
                WriteLine(t);

            WriteLine();

            foreach (var t in countries.Where(x => !x.StartsWith('П')))
                WriteLine(t);
        }
    }
}
