using NHibernate;
using System;
using System.Threading.Tasks;

namespace Techtalk.FM.Domain.Contracts.Repositories
{
    /// <summary>
    /// UnitOfWork operations
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Check if Connection is Open
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// Open a NHibernate Session
        /// </summary>
        /// <returns>Current IUnitOfWork instance</returns>
        IUnitOfWork Open(bool setCommitFlushMode = false);

        /// <summary>
        /// Close Session
        /// </summary>
        void Close();

        /// <summary>
        /// Starts a transaction on the current Session
        /// </summary>
        /// <param name="isolation">Isolation Level</param>
        /// <returns>Current Transaction instance</returns>
        ITransaction BeginTransaction(string isolation = null);

        /// <summary>
        /// RollBack Transaction
        /// </summary>
        /// <returns>A Task indicates the operation is done</returns>
        Task RollbackAsync();

        /// <summary>
        /// Commit Transaction
        /// </summary>
        /// <returns>A Task indicates the operation is done</returns>
        Task CommitAsync();

        /// <summary>
        /// Get current ISession
        /// </summary>
        /// <returns>Current ISession</returns>
        object GetSession();
    }
}
