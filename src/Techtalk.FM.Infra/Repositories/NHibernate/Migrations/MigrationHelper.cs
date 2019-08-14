using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Techtalk.FM.Domain.Contracts.Migrations;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    public class MigrationHelper : IMigrationHelper
    {
        #region "  Properties  "

        private string _tag;
        private IConfiguration _configuration;
        private BaseMigrationContext _migrationContext;

        #endregion

        #region "  Constructors  "

        public MigrationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tag = _configuration.GetSection("Migration")["TagName"];
            _migrationContext = new BaseMigrationContext(configuration.GetConnectionString("TechtalkConn"));
        }

        #endregion

        #region "  IMigrationHelper  "

        /// <summary>
        /// Run migration Up
        /// </summary>
        public void RunMigrationUp()
        {
            //Configure Migration Runner                
            ServiceProvider service = CreateService(_migrationContext);

            using (IServiceScope scope = service.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Run Migration Down
        /// </summary>
        public void RunMigrationDown()
        {
            //Configure Migration Runner
            ServiceProvider service = CreateService(_migrationContext);

            using (IServiceScope scope = service.CreateScope())
            {
                DownDatabase(scope.ServiceProvider);
            }
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Get ServiceProvider of IMigrationRunner to Migrate Database
        /// </summary>
        /// <param name="serviceProvider">IServiceProvider injector</param>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        /// <summary>
        /// Get ServiceProvider of IMigrationRunner to Migrate Database Down
        /// </summary>
        /// <param name="serviceProvider">IServiceProvider injector</param>
        private static void DownDatabase(IServiceProvider serviceProvider)
        {
            IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateDown(0);
        }

        /// <summary>
        /// Create Migration service
        /// </summary>
        /// <param name="context">Migration context</param>
        /// <returns>ServiceProvider</returns>
        private ServiceProvider CreateService(BaseMigrationContext context)
        {
            ServiceProvider service = new ServiceCollection()
                            .AddFluentMigratorCore()
                            .ConfigureRunner(r => r
                                .AddPostgres()
                                .WithGlobalConnectionString(context.ConnectionString)
                                .WithMigrationsIn(typeof(BaseMigrationContext).Assembly)
                                .WithVersionTable(new VersionTable(_configuration))
                            )
                            //Configure to run only migrations that is marked with context tag
                            .Configure<RunnerOptions>(opt =>
                            {
                                opt.Tags = new[] { _tag };
                            })
                            .BuildServiceProvider(true);
            return service;
        }

        #endregion
    }
}
