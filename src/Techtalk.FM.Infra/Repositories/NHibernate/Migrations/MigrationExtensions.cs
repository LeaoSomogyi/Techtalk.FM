using FluentMigrator.Runner;
using System;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    /// <summary>
    /// Utils Migration Extensions
    /// </summary>
    public static class MigrationExtensions
    {
        /// <summary>
        /// Get the correct database provider from the Migration Context
        /// </summary>
        /// <param name="runnerBuilder">IMigrationRunnerBuilder</param>
        /// <param name="context">BaseMigrationContext</param>
        /// <returns>IMigrationRunnerBuilder with correct database configured</returns>
        public static IMigrationRunnerBuilder GetDatabase(this IMigrationRunnerBuilder runnerBuilder,
            BaseMigrationContext context)
        {
            switch (context.DatabaseProvider)
            {
                case "postgres":
                    return runnerBuilder.AddPostgres();
                case "sqlserver":
                    return runnerBuilder.AddSqlServer();
                case "sqlite":
                    return runnerBuilder.AddSQLite();
                case "firebird":
                    return runnerBuilder.AddFirebird();
                default:
                    throw new ArgumentException("Unable to define database provider");
            }
        }
    }
}
