using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.Core.Application.Exceptions;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Domain.Commons;
using TaskManagement.Infraestructure.Persistence.Context;

namespace TaskManagement.Infraestructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _dbContext;
        protected DbSet<T> Entities;
        protected readonly IConfiguration Configuration;

        public BaseRepository(ApplicationContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<T>();
            Configuration = configuration;
        }

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        public virtual async Task<T> CreateAsync(T entity)
        {
            try
            {
                await Entities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity is AuditoryProperties entityModel)
                {
                    entityModel.IsDeleted = true; // Soft delete logic (mark as deleted)
                    Entities.Update(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        public virtual async Task UpdateAsync(T entity, int id)
        {
            try
            {
                var entry = await Entities.FindAsync(id);
                if (entry != null)
                {
                    _dbContext.Entry(entry).CurrentValues.SetValues(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await Entities.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }


        }

        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await Entities.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }

        }
    }
}
