namespace Todo.Core.Requests.Chores;

public class GetChoreByIdRequest : BaseRequest
{
    public long Id { get; set; }
}