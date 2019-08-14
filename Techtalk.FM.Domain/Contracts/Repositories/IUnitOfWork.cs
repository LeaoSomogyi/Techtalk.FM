using NHibernate;
using System;
using System.Threading.Tasks;

namespace Techtalk.FM.Domain.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsOpen { get; }

        IUnitOfWork Open(bool setCommitFlushMode = false);

        void Close();

        ITransaction BeginTransaction(string isolation = null);

        Task RollbackAsync();

        Task CommitAsync();

        object GetSession();
    }
}
