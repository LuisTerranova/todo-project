using System.Text.Json.Serialization;

namespace Todo.Core.Responses;

//Response envelope to standardise responses
//Usage of TData generic to send any kind of object
public class Response<TData>
{
    //Usage of defaultstatuscode to manage bad and ok requests
    private readonly int _code = Configuration.DefaultStatusCode;
    /*Usage of jsonconstructor to specify the constructor that creates JSON
    that will be sent to frontend*/
    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;
    //Constructor for data manipulation inside API
    public Response(TData data, int code = Configuration.DefaultStatusCode, string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }
    //Response properties, data, message and issuccess statuscode
    public TData? Data { get; set; }
    public string? Message { get; set; }
    //Ignoring possibly redundant data on JSON
    [JsonIgnore]
    public bool IsSuccess
        => _code is >= 200 and <= 299;
}