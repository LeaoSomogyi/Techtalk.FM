namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    /// <summary>
    /// Migration Context to be used when Migrate Database
    /// </summary>
    public class BaseMigrationContext
    {
        /// <summary>
        /// Connection String to use when migrate database
        /// </summary>
        public readonly string ConnectionString;

        /// <summary>
        /// Database provider
        /// </summary>
        public readonly string DatabaseProvider;

        /// <summary>
        /// Base Constructor of Migration Contexts
        /// </summary>
        /// <param name="connString">Connection string of the context</param>
        /// <param name="provider">Database provider</param>
        public BaseMigrationContext(string connString, string provider)
        {
            ConnectionString = connString;
            DatabaseProvider = provider;
        }
    }
}
