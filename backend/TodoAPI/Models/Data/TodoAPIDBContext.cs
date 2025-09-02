using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Models.Data
{
    public class TodoAPIDBContext : DbContext
    {
        public TodoAPIDBContext(DbContextOptions<TodoAPIDBContext> options) : base(options)
        {

        }
        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Adds an indexing to the due dates for faster querying when filtering.
            builder.Entity<TodoItem>().HasIndex(t => t.DueDate);
            // initialDate is just a placeholder to avoid using dynamic values.
            DateTime initialDate = new DateTime(1995, 1, 1);
            builder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = 1,
                    Title = "Todo item number one",
                    Description = "this is the first item in the todo list.",
                    CreatedDate = initialDate,
                    DueDate = initialDate,
                    IsDone = false
                },
                new TodoItem
                {
                    Id = 2,
                    Title = "Second todo item",
                    Description = "this is the second item in the todo list.",
                    CreatedDate = initialDate,
                    DueDate = initialDate,
                    IsDone = true
                }
            );


        }
    }
}
