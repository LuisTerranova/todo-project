using System.Text.Json.Serialization;

namespace Todo.Core.Responses;
//Creation of a PagedResponse to handle pagination easily
public class PagedResponse<TData> : Response<TData> //Inheriting from Response
{
    /*JSON constructor annotation explicitly put on the paginated constructor
    that will send out data*/
    [JsonConstructor]
    public PagedResponse(
        TData data, 
        int totalCount, 
        int currentPage = 1, 
        int pageSize = Configuration.DefaultPageSize) : base(data)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    //Constructor for data manipulation inside API
    public PagedResponse(
        TData data,
        string? message,
        int code = Configuration.DefaultStatusCode)
        :base(data, code, message)
    {
        
    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount/(double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount { get; set; }
}