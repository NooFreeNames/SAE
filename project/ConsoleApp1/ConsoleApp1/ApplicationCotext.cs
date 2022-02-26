using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    internal class ApplicationCotext: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public ApplicationCotext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=helloapp.db");
        }
    }
}
