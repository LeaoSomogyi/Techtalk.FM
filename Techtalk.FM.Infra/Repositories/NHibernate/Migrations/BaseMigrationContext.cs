namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    public class BaseMigrationContext
    {
        /// <summary>
        /// Connection String to use when migrate database
        /// </summary>
        public readonly string ConnectionString;

        /// <summary>
        /// Base Constructor of Migration Contexts
        /// </summary>
        /// <param name="connString">Connection string of the context</param>
        /// <param name="tagName">Tag name of the context</param>
        public BaseMigrationContext(string connString)
        {
            ConnectionString = connString;
        }
    }
}
