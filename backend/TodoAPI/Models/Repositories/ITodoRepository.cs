namespace TodoAPI.Models.Repositories
{
    public interface ITodoRepository
    {
        TodoItem GetById(int id);
        List<TodoItem> GetAll();

        void CreateTodo(TodoItem item);
        void UpdateTodo(TodoItem item);
        void DeleteTodo(int id);

    }
}
