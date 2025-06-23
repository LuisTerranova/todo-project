using System.ComponentModel.DataAnnotations;
using Todo.Core.Common;

namespace Todo.Core.Requests.Chores;

public class UpdateChoreRequest : BaseRequest
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(80, ErrorMessage = "Your chore title can't exceed 80 characters")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(255, ErrorMessage = "Your chore description can't exceed 255 characters")]
    public string Description { get; set; }
    [FutureDate]
    public DateTime? DueDate { get; set; }
}