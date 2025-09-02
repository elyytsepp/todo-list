using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
    public class UpdateTodoDTO
    {
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
