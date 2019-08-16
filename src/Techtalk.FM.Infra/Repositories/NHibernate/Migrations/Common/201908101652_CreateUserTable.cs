using FluentMigrator;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    [Migration(201908101652)]
    public class CreateUserTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("admin_user")
                .WithColumn("name").AsString(250).NotNullable()
                .WithColumn("email").AsString(100).NotNullable()
                .WithColumn("user_password").AsString(int.MaxValue).NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            IfDatabase("SqlServer", "Postgres")
                .Create.Column("id").OnTable("admin_user").AsGuid().PrimaryKey();

            IfDatabase("Firebird")
                .Create.Column("id").OnTable("admin_user").AsString(38).PrimaryKey();
        }
    }
}
