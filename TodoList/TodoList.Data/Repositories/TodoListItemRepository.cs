using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Models;

namespace TodoList.Data.Repositories
{
    public class TodoListItemRepository
    {
        private readonly TodoListDbContext _context;
        public TodoListItemRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<TodoListItem> Add(TodoListItem item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
