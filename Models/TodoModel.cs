using Todo.Controllers;

namespace Todo.Models;

public class TodoModel
{
    public TodoModel() { }  
    
    public TodoModel(string title, string body, int id)  
    {  
        Title = title;  
        Body = body;
        UserId = id;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}