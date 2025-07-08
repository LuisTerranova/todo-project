namespace Todo.Core.Models;
//Chore class, the core class of the app
public class Chore
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; } =  DateTime.UtcNow;
    public bool IsDone { get; set; }
    public string UserId { get; set; } 
}