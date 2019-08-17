using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts.Migrations;

namespace Techtalk.FM.Console
{
    public class MigrationRunner : IHostedService, IDisposable
    {
        private readonly IMigrationHelper _migrationHelper;

        public MigrationRunner(IMigrationHelper migrationHelper)
        {
            _migrationHelper = migrationHelper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _migrationHelper.RunMigrationUp();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
