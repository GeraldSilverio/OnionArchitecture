namespace TaskManagement.Core.Application.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity managed by the repository.</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity after it has been persisted.</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously based on its ID.
        /// </summary>
        /// <param name="entity">The entity with updated data.</param>
        /// <param name="id">The ID of the entity to update.</param>
        Task UpdateAsync(T entity, int id);

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity with the specified ID.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all entities of type T asynchronously.
        /// </summary>
        /// <returns>A list of all entities of type T.</returns>
        Task<List<T>> GetAllAsync();
    }
}
