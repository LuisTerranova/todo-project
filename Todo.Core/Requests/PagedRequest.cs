namespace Todo.Core.Requests;
//Paged request to handle pagination inside requests
public class PagedRequest : BaseRequest
{
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}