namespace Todo.Core.Requests.Chores;

public class DeleteChoreRequest : BaseRequest
{
    public long Id { get; set; }
}