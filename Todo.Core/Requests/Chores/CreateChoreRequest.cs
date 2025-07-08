using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Requests.Chores;
//Create chore request with data annotations to facilitate form handling
public class CreateChoreRequest : BaseRequest
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(80, ErrorMessage = "Your chore title can't exceed 80 characters")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(255, ErrorMessage = "Your chore description can't exceed 255 characters")]
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
}