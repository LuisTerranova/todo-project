using Todo.Api.Common.Api;
using Todo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.MapGet("/", () => "Hello World!");
app.UseSecurity();
app.MapEndpoints();

app.Run();
