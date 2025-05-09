using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModels;

public class TodoViewModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
}