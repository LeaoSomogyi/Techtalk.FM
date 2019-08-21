using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations.SQLServer
{
    [Migration(201908101832)]
    [Tags("sqlserver")]
    public class CreateUserIndex : Migration
    {
        public override void Up()
        {
            Create.Index("ix_admin_user_email_password").OnTable("admin_user")
                .OnColumn("email").Ascending()
            .WithOptions().NonClustered()
                .Include("id")
                .Include("name")
                .Include("created_at")
                .Include("updated_at");
        }

        public override void Down()
        {
            Delete.Index("ix_admin_user_email_password").OnTable("admin_user");
        }
    }
}
