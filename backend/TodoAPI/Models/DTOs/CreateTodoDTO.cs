using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
    public class CreateTodoDTO
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
