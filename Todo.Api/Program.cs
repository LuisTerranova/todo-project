using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Endpoints;
using Todo.Api.Handlers;
using Todo.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization();

builder.Services.AddTransient<IChoreHandler, ChoreHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName); //Melhor para documentar metodos de nomes repetidos
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.MapEndpoints();

app.Run();
