using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Models;

namespace Todo.Mappings;

//separating the todo mapping from the dbcontext to develop sense of organization in folders
//preparing for bigger problems with harder relations
public class TodoMap : IEntityTypeConfiguration<TodoModel>
{
    public void Configure(EntityTypeBuilder<TodoModel> builder)
    {
        builder.ToTable("Todos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd()
                                              .UseIdentityColumn();

        builder.Property(x => x.Title).IsRequired()
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Body).HasColumnName("Body")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);

        builder.Property(x => x.IsCompleted).HasColumnName("IsCompleted")
            .HasColumnType("bit")
            .HasDefaultValue(false);
        
        builder.HasOne(x=>x.User)
            .WithMany(x=>x.Todos)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}