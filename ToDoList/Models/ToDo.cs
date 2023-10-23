using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models;

public class ToDo
{
    [Key]
    public int ToDoId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [DisplayName("Due Date")]
    public DateTime DueDate { get; set; } = DateTime.Now;
}