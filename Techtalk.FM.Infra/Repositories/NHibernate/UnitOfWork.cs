using Microsoft.Extensions.Configuration;
using NHibernate;
using System;
using System.Data;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts.Repositories;

namespace Techtalk.FM.Infra.Repositories.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed { get; set; }
        private NHContext _context { get; set; }
        private ISession _session { get; set; }
        private ITransaction _transaction { get; set; }

        public UnitOfWork(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            string conn = configuration.GetConnectionString("TechtalkConn");
            string schema = configuration.GetSection("Provider")["DefaultSchema"];
            string provider = configuration.GetSection("Provider")["ProviderName"];

            _serviceProvider = serviceProvider;

            _context = new NHContext(conn, schema, provider);
        }

        public bool IsOpen { get { return !_disposed && _session != null && _session.IsOpen; } }

        public IUnitOfWork Open(bool setCommitFlushMode = false)
        {
            if (_disposed) return this;

            _session = _context.Configure().OpenSession();

            if (setCommitFlushMode)
                _session.FlushMode = FlushMode.Commit;

            return this;
        }

        public void Close()
        {
            Dispose();
        }

        public ITransaction BeginTransaction(string isolation = null)
        {
            IsolationLevel? _isolation = null;

            if (!string.IsNullOrWhiteSpace(isolation))
                _isolation = (IsolationLevel)Enum.Parse(typeof(IsolationLevel), isolation, true);

            if (_disposed)
                return default;

            if (_session == null || !_session.IsOpen)
            {
                Open();
            }
            else
            {
                if (_transaction != null)
                    _transaction.Dispose();
            }

            _transaction = _isolation.HasValue ? _session.BeginTransaction(_isolation.Value) : _session.BeginTransaction();

            return _transaction;
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                _session.Clear();
                _transaction.Dispose();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();

                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        
        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                        _transaction.Dispose();

                    if (_session != null && _session.IsOpen)
                        _session.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsDispose()
        {
            return _disposed;
        }

        public object GetSession()
        {
            if (_disposed)
                return default(ISession);

            if (_session == null || !_session.IsOpen)
                Open();

            return _session;
        }
    }
}
