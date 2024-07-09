using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Infraestructure.Persistence.EntitiesConfiguration;


namespace TaskManagement.Infraestructure.Persistence.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskStatusConfiguration());

    }

    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<TasksStatus> TasksStatus { get; set; }


}