namespace Todo.Core.Requests;
//BaseRequest class with UserId property that requests will use
//Created to be inherited from
public class BaseRequest
{
    public string UserId { get; set; }
}