namespace Todo.Core;

public class Configuration
{
    //Configuration constants to reuse on the code, mainly for paged responses
    public const int DefaultStatusCode = 200;
    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;
    //Addition of the connectionstring on the configs
    public static string ConnectionString { get; set; } = string.Empty;
}