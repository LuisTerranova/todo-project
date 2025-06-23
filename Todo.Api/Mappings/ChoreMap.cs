using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Core.Models;

namespace Todo.Api.Mappings;

public class ChoreMap : IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.ToTable("Chores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(80);
        
        builder.Property(x => x.Description)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x=>x.DueDate)
            .HasColumnType("DATE")
            .IsRequired(false);
        
        builder.Property(x=>x.CreatedDate)
            .HasColumnType("DATE")
            .IsRequired();

        builder.Property(x => x.IsDone)
            .HasColumnType("BIT")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
    }
}