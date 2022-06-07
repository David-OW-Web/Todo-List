using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TodoList.Data.Models;
using TodoList.Data.Repositories;
using ToDo = TodoList.Data.Models.TodoList;

namespace TodoList.WebApp.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly TodoListRepository _todoListRepository;
        private readonly TodoListItemRepository _todoListItemRepository;
        private string? _userId;
        public TodoListController(TodoListRepository todoListRepository, TodoListItemRepository todoListItemRepository)
        {
            _todoListRepository = todoListRepository;
            _todoListItemRepository = todoListItemRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            _userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = _userId;
        }

        /// <summary>
        /// Gibt eine Tabellen-Ansicht mit allen Listen zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Rufe alle Listen ab, aktiv und inaktiv
            List<ToDo> todoLists = await _todoListRepository.GetAllFromUser(_userId);
            // Sortiere nach Aktiv oder inaktiv, dann nach Erstelldatum
            todoLists = todoLists.OrderByDescending(t => t.Active).ThenByDescending(t => t.CreatedAt).ToList();
            // Nur aktive Listen
            todoLists = todoLists.Where(t => t.Active).ToList();

            return View(todoLists);
        }

        /// <summary>
        /// Gibt eine Ansicht mit der Todo-Liste und ihren Items zurück
        /// </summary>
        /// <param name="id">Id der Liste</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Rufe Liste ab
            ToDo todoList = await _todoListRepository.Get(id);

            // Prüfe ob Liste existiert
            if(todoList == null)
            {
                return View("ListNotFound");
            }

            return View(todoList);
        }

        /// <summary>
        /// Zeigt das Formular an zum Erstellen einer Checkliste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Verarbeitet die Daten beim Erstellen einer Todo-Liste
        /// </summary>
        /// <param name="toDo">Eine Instanz des Todo-Modells</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ToDo toDo)
        {
            if(ModelState.IsValid)
            {
                ToDo newToDo = await _todoListRepository.Add(toDo);
                // Leite den Benutzer weiter zur Detailansicht der Liste, wenn alles valid ist.
                return RedirectToAction("Details", new { id = newToDo.Id });
            }
            return View(toDo);
        }

        /// <summary>
        /// Gibt die Ansicht zurück, zum ein Item zur Liste hinzufügen
        /// </summary>
        /// <param name="id">Die Liste, welche ein Item bekommen soll</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AddItem(int? id)
        {
            // Prüfe ob id null ist, wenn null zeige Fehleransicht
            if(id == null)
            {
                return View("NoListSelected");
            }

            await SetViewData(id.Value);
            return View();
        }

        /// <summary>
        /// Verarbeitet die Daten beim Hinzufügen eines Items
        /// </summary>
        /// <param name="id">Id der Liste</param>
        /// <param name="item">Instanz eines Item Modells</param>
        /// <returns></returns>
        [HttpPost]
        // Ohne Bind wird eine Exception verursacht wegen "Identity_Insert set to off"
        public async Task<IActionResult> AddItem(int? id, [Bind("Title,Done,CreatedAt,TodoListId")]TodoListItem item)
        {
            if(ModelState.IsValid)
            {
                // Alles ist valid, schicke Benutzer zurück zur Detailansicht
                await _todoListItemRepository.Add(item);
                return RedirectToAction("Details", new { id = item.TodoListId });
            }
            await SetViewData(id.Value);
            return View(item);
        }

        /// <summary>
        /// Zeigt die Ansicht zum Löschen einer Todo-Liste an
        /// </summary>
        /// <param name="id">Id der zu löschenden Todo-Liste</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ToDo toDo = await _todoListRepository.Get(id);
            if (toDo == null) return View("ListNotFound");

            return View(toDo);
        }

        /// <summary>
        /// Verarbeitet das Löschen einer Todo-Liste. Setzt Active auf false
        /// </summary>
        /// <param name="id">Die Id der Todo-Liste</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, ToDo toDoList)
        {
            ToDo toDo = await _todoListRepository.Get(id);
            if (toDo == null) return View("ListNotFound");

            toDo.Active = false;
            await _todoListRepository.Update(toDo);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Gibt die Select-List von Todo-Listen zurück für die Seite AddItem
        /// </summary>
        /// <param name="id">Id von der Todo-Liste</param>
        /// <returns></returns>
        private async Task SetViewData(int id)
        {
            List<ToDo> todoLists = await _todoListRepository.GetAllFromUser(_userId);

            todoLists = todoLists.Where(t => t.Id == id).ToList();

            IEnumerable<SelectListItem> selectListItems = todoLists.Select(x => new SelectListItem
            {
                Selected = x.Id == id,
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewData["TodoListId"] = selectListItems;
        }
    } 
}
