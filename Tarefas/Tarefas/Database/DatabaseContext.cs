using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tarefas.Entities;

namespace Tarefas.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ToDo> Todos { get; set; }

        public DatabaseContext() {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Constants.DatabasePath}");
        }
    }
}
