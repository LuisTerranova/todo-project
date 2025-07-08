namespace Todo.Core.Requests.Chores;
//Get by id with needed fields, and inheriting from base request
public class GetChoreByIdRequest : BaseRequest
{
    public long Id { get; set; }
}