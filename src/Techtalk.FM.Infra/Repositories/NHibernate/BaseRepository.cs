using NHibernate;
using NHibernate.Criterion;
using System;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts;
using Techtalk.FM.Domain.Contracts.Repositories;

namespace Techtalk.FM.Infra.Repositories.NHibernate
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        #region "  Properties  "

        protected ISession Session { get; set; }

        #endregion

        #region "  Constructors  "

        protected BaseRepository() { }

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            Session = unitOfWork.GetSession() as ISession;
        }

        #endregion

        #region "  IRepository  "

        /// <summary>
        /// Insert Entity Async
        /// </summary>
        /// <param name="entity">Object to be inserted</param>
        /// <returns>Saved entity</returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                var added = await Session.SaveAsync(entity);
                return await Session.GetAsync(entity.GetType(), added) as T;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Entity Async
        /// </summary>
        /// <param name="entity">Object to be deleted</param>
        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                await Session.DeleteAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Entity by Id Async
        /// </summary>
        /// <param name="id">UniqueIdentifier Id</param>
        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetAsync(id);

                await DeleteAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Entity by Id Async
        /// </summary>
        /// <param name="id">UniqueIdentifier Id</param>
        /// <returns>Object correspondent to Id</returns>
        public virtual async Task<T> GetAsync(Guid id)
        {
            try
            {
                var result = await Session.CreateCriteria<T>()
                    .Add(Restrictions.Eq("Id", id))
                    .UniqueResultAsync<T>();

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Save or Update Entity Async
        /// </summary>
        /// <param name="entity">Object. If not exists, save, else update</param>
        /// <returns>Saved or Updated Object</returns>
        public virtual async Task<T> SaveAsync(T entity)
        {
            try
            {
                return await Session.MergeAsync(entity);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Update Entity Async
        /// </summary>
        /// <param name="entity">Object to be updated</param>
        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                await Session.UpdateAsync(entity);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
