using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Database;
using Tarefas.Entities;

namespace Tarefas.Repositories
{
    public class TodosRepository
    {
        private DatabaseContext Database { get; set; }

        public TodosRepository()
        {
            Database = new DatabaseContext();
        }

        public async Task<List<ToDo>> Search(DateTime data) 
        {
           return await Database.Todos.Where(item =>
                item.Date.HasValue &&
                item.Date.Value.Year == data.Year &&
                item.Date.Value.Month == data.Month &&
                item.Date.Value.Day == data.Day
            ).ToListAsync();
        }

        public async Task<bool> Create(ToDo todo)
        {
            Database.Todos.Add(todo);
            int affectedRows = await Database.SaveChangesAsync();

            return (affectedRows > 0);
        }

        public async Task<bool> Update(ToDo todo)
        {
            Database.Todos.Update(todo);
            int affectedRows = await Database.SaveChangesAsync();

            return (affectedRows > 0);

        }

        public async Task<bool> Delete(int id)
        {
            ToDo todo = await GetById(id);
            Database.Todos.Remove(todo);
            int affectedRows = await Database.SaveChangesAsync();

            return (affectedRows > 0);
        }

        public async Task<ToDo> GetById(int id)
        {
            return await Database.Todos.FindAsync(id);
        }
    }
}
