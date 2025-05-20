using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Todo.ViewModels;

public class TodoViewModel(string title, string description)
{
    [Required (ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.", MinimumLength = 2)]
    public string Title { get; set; } = title;

    [Display(Name = "Description")]
    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.", MinimumLength = 2)]
    public string Description { get; set; } = description;

    public bool IsCompleted { get; set; }
}