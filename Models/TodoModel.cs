namespace Todo.Models;

public class TodoModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsCompleted { get; set; }
}