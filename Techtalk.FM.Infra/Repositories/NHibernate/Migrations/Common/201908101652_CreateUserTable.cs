using FluentMigrator;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    [Migration(201908101652)]
    public class CreateUserTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(250).NotNullable()
                .WithColumn("email").AsString(100).NotNullable()
                .WithColumn("password").AsString(int.MaxValue).NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();
        }
    }
}
