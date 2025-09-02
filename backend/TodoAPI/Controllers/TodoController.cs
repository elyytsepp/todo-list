using TodoAPI.Models;
using TodoAPI.Models.Repositories;
using TodoAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/todo
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll(
            [FromQuery] bool? isDone,
            [FromQuery] DateTime? dueBefore,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Gets all the todos from the database.
            var todos = _todoRepository.GetAll().AsQueryable();
            // If any filters are provided in the query, apply them and return the corresponding todos.
            if (isDone.HasValue)
            {
                // Filters for Done/ Not done todos.
                todos = todos.Where(t => t.IsDone == isDone.Value);
            }
            if (dueBefore.HasValue)
            {
                // Filtes for todos that have their due dates BEFORE the date given to the filter.
                todos = todos.Where(t => t.DueDate <= dueBefore.Value);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                // Filters for todos that include the search phrase in their description.
                todos = todos.Where(t => t.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // Gives total number of pages after filtering.
            var totalCount = todos.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Applies pagination to limit the number of todos on one page.
            var pagedTodos = todos
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Return a paginated response.
            var response = new
            {
                currentPage = page,
                totalPages,
                pageSize,
                totalCount,
                todos = pagedTodos
            };
                
            // Returns the list of todos with or without the filters and loads 10 per page.
            return Ok(response);
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var todo = _todoRepository.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoDTO item)
        {
            var newTodo = new TodoItem
            {
                Title = item.Title,
                Description = item.Description,
                IsDone = false,
                CreatedDate = DateTime.UtcNow,
                DueDate = item.DueDate
            };
            _todoRepository.CreateTodo(newTodo);
            return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTodoDTO item)
        {
            var oldItem = _todoRepository.GetById(id);
            if (oldItem == null)
            {
                return NotFound();
            }
            oldItem.Description = item.Description;
            oldItem.DueDate = item.DueDate;
            oldItem.IsDone = item.IsDone;

            _todoRepository.UpdateTodo(oldItem);
            return Ok(oldItem);
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _todoRepository.GetById(id);
            if(item == null)
            {
                return NotFound();
            }
            _todoRepository.DeleteTodo(id);
            return NoContent();
        }

    }
}
