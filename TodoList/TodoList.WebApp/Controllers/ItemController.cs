using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Models;
using TodoList.Data.Repositories;

namespace TodoList.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly TodoListItemRepository _itemRepository;
        private readonly TodoListRepository _todoListRepository;

        public ItemController(TodoListItemRepository itemRepository, TodoListRepository todoListRepository)
        {
            _itemRepository = itemRepository;
            _todoListRepository = todoListRepository;
        }

        /// <summary>
        /// Markiert ein Todo-Item als erledigt
        /// </summary>
        /// <param name="id">Die Id des Todo-Items</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Done(int id)
        {
            // Rufe Item ab und prüfe ob es existiert
            TodoListItem item = await _itemRepository.Get(id);

            if (item == null) return NotFound();

            // Setze es auf erledigt
            item.Done = true;

            await _itemRepository.Update(item);

            return RedirectToAction("Details", "TodoList", new { id = item.TodoListId });
        }
    }
}
