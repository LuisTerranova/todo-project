using Todo.Models;
using Microsoft.EntityFrameworkCore;
using Todo.Mappings;

namespace Todo.Data;

public class TodoDataContext : DbContext
{
    public DbSet<TodoModel> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer
        ("Server=localhost,1433;Database=TodoDb;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoMap());
    }
}