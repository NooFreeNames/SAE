using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            using (var db = new ApplicationCotext())
            {
                User tom = new User { Name = "Tom", Age = 33};
                User alice = new User { Name = "Alice", Age = 26 };
                db.Users.Add(tom);
                db.Users.Add(alice);
                db.SaveChanges();
                Console.WriteLine("SaveChanges");

                var users = db.Users.ToList();
                Console.WriteLine("Список объектов: ");
                foreach(var user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name}.{user.Age}");
                }
            }
        }
    }
}