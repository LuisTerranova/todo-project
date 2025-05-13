namespace Todo.Models;

public class TodoModel(string title, string body)
{
    public int Id { get; set; }
    public string Title { get; set; } = title;
    public string Body { get; set; } = body;
    public bool IsCompleted { get; set; }
}