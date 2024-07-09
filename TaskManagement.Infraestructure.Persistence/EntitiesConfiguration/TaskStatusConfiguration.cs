using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TasksStatus = TaskManagement.Core.Domain.Entities.TasksStatus;

namespace TaskManagement.Infraestructure.Persistence.EntitiesConfiguration;

public class TaskStatusConfiguration : IEntityTypeConfiguration<TasksStatus>
{
    public void Configure(EntityTypeBuilder<TasksStatus> builder)
    {
        builder.ToTable("TaskStatus");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(30);
        builder.HasIndex(x => x.Name).IsUnique();


        builder.HasData(
            new TasksStatus[]
            {
                new TasksStatus()
                {
                    Id = 1,
                    Name = "New",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsDeleted = false,
                },
                new TasksStatus()
                {
                    Id = 2,
                    Name = "In Progress",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsDeleted = false,
                },
                new TasksStatus()
                {
                    Id = 3,
                    Name = "Completed",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "N/A",
                    IsDeleted = false,
                }
            }
        );
    }
}