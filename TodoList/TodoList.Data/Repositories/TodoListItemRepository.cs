using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Models;
using ToDo = TodoList.Data.Models.TodoList;

namespace TodoList.Data.Repositories
{
    public class TodoListItemRepository
    {
        private readonly TodoListDbContext _context;
        public TodoListItemRepository(TodoListDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gibt ein spezifisches Todo-Item zurück
        /// </summary>
        /// <param name="id">Id des Todo-Items</param>
        /// <returns></returns>
        public async Task<TodoListItem> Get(int id)
        {
            return await _context.TodoListItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Erstellt ein Todo-Item
        /// </summary>
        /// <param name="item">Instanz eines Todo-Item Modells</param>
        /// <returns></returns>
        public async Task<TodoListItem> Add(TodoListItem item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();

            // Aktualisiere den Wert vom Aktualisierungsdatum der Liste

            await UpdateTodo(item.TodoListId.Value);

            return item;
        }

        /// <summary>
        /// Aktualisiert ein Todo-Item
        /// </summary>
        /// <param name="item">Instanz eines Todo-Item Modells</param>
        /// <returns></returns>
        public async Task<int> Update(TodoListItem item)
        {
            _context.Update(item);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Aktualisiert das Aktualisierungsdatum einer Todo-Liste
        /// </summary>
        /// <param name="id">Id der Liste</param>
        /// <returns></returns>
        private async Task UpdateTodo(int id)
        {
            ToDo toDo = await _context.TodoLists.FindAsync(id);
            toDo.UpdatedAt = DateTime.Now;
            _context.Update(toDo);
            await _context.SaveChangesAsync();
        }
    }
}
