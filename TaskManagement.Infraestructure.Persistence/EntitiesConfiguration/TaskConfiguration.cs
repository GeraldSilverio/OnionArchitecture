using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Infraestructure.Persistence.EntitiesConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.ToTable("Task");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(60);



        builder.HasOne(x => x.TaskStatus)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.IdTaskStatus)
            .OnDelete(DeleteBehavior.Cascade);
    }
}