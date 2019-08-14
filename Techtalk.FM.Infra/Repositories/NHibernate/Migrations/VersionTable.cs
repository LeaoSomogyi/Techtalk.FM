using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Configuration;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        private readonly IConfiguration configuration;

        public string ColumnName => "version";

        public string SchemaName => configuration.GetSection("Provider")["DefaultSchema"];

        public string TableName => "versioninfo";

        public string UniqueIndexName => "uc_version";

        public string AppliedOnColumnName => "applied_on";

        public string DescriptionColumnName => "description";

        public object ApplicationContext { get; set; }

        public virtual bool OwnsSchema => true;

        public VersionTable(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
