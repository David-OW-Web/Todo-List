using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo = TodoList.Data.Models.TodoList;

namespace TodoList.Data.Repositories
{
    public class TodoListRepository
    {
        private readonly TodoListDbContext _context;
        public TodoListRepository(TodoListDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gibt eine Liste mit allen TodoListen zurück (aktiv und inaktiv)
        /// </summary>
        /// <returns></returns>
        public async Task<List<ToDo>> GetAllFromUser(string userId)
        {
            return await _context.TodoLists
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Gibt eine Todo-Liste mit ihren Items zurück
        /// </summary>
        /// <param name="id">Die Id der gewünschten Todo-Liste</param>
        /// <returns></returns>
        public async Task<ToDo?> Get(int id)
        {
            return await _context.TodoLists
                .Include(t => t.User)
                .Include(t => t.TodoListItems)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Erstellt eine neue Todo-Liste
        /// </summary>
        /// <param name="toDo">Eine Instanz eines Todo-Listen Modells</param>
        /// <returns></returns>
        public async Task<ToDo> Add(ToDo toDo)
        {
            _context.TodoLists.Add(toDo);
            await _context.SaveChangesAsync();
            return toDo;
        }

        /// <summary>
        /// Aktualisiert eine Todo-Liste
        /// </summary>
        /// <param name="toDo">Eine Instanz des Todo-Listen Modells</param>
        /// <returns></returns>
        public async Task<int> Update(ToDo toDo)
        {
            _context.TodoLists.Update(toDo);
            return await _context.SaveChangesAsync();
        }
    }
}
