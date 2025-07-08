using Todo.Api.Common.Api;
using Todo.Api.Endpoints;
//creation of builder to add necessary dependencies
var builder = WebApplication.CreateBuilder(args);

//adding necessary dependencies
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddDocumentation();
builder.AddServices();
//building the application
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.MapGet("/", () => "Hello World!");
app.UseSecurity();
app.MapEndpoints();

app.Run();
