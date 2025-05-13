namespace Todo.Models;

public class TodoModel
{
    public TodoModel() { }  
    
    public TodoModel(string title, string body)  
    {  
        Title = title;  
        Body = body;  
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsCompleted { get; set; }
}