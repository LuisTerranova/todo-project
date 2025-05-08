namespace Todo.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsComplete { get; set; }
}