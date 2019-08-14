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
            Create.Index("ix_users_email_password").OnTable("users")
                .OnColumn("email").Ascending()
                .OnColumn("password").Ascending()

            .WithOptions().NonClustered()
                .Include("id")
                .Include("name")
                .Include("save_date")
                .Include("update_date");
        }

        public override void Down()
        {
            Delete.Index("ix_users_email_password").OnTable("users");
        }
    }
}
