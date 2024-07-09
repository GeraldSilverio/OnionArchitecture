using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Infraestructure.Persistence.Context;

namespace TaskManagement.Infraestructure.Persistence.Repositories
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        private readonly ApplicationContext _dbContext;

        public TasksRepository(ApplicationContext dbContext, IConfiguration configuration) : base(dbContext,
            configuration)
        {
            _dbContext = dbContext;
        }

        public override async Task<List<Tasks>> GetAllAsync()
        {
            return await _dbContext.Tasks.Include("TaskStatus").Where(x => !x.IsDeleted).ToListAsync();
        }
        public override async Task<Tasks> GetByIdAsync(int id)
        {
            return await _dbContext.Tasks.Include("TaskStatus").Where(x=> !x.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}