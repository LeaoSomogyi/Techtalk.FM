using System;
using System.Threading.Tasks;

namespace Techtalk.FM.Domain.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Insert Entity Async
        /// </summary>
        /// <param name="entity">Object to be inserted</param>
        /// <returns>Saved entity</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Delete Entity Async
        /// </summary>
        /// <param name="entity">Object to be deleted</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Delete Entity by Id Async
        /// </summary>
        /// <param name="id">UniqueIdentifier Id</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get Entity by Id Async
        /// </summary>
        /// <param name="id">UniqueIdentifier Id</param>
        /// <returns>Object correspondent to Id</returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Save or Update Entity Async
        /// </summary>
        /// <param name="entity">Object. If not exists, save, else update</param>
        /// <returns>Saved or Updated Object</returns>
        Task<T> SaveAsync(T entity);

        /// <summary>
        /// Update Entity Async
        /// </summary>
        /// <param name="entity">Object to be updated</param>
        Task UpdateAsync(T entity);
    }
}
