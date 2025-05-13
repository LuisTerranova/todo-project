using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModels;

public class TodoViewModel
{
    [Required (ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.", MinimumLength = 2)]
    public string Title { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}