using TodoAPI.Models.Data;

namespace TodoAPI.Models.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAPIDBContext _context;
        public TodoRepository(TodoAPIDBContext context)
        {
            _context = context;
        }

        public void CreateTodo(TodoItem item)
        {
            _context.Todos.Add(item);
            _context.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
        }

        public List<TodoItem> GetAll()
        {
            return _context.Todos.ToList();
        }

        public TodoItem GetById(int id)
        {
            return _context.Todos.Find(id);
        }

        public void UpdateTodo(TodoItem item)
        {
            _context.Todos.Update(item);
            _context.SaveChanges();
        }
    }
}
