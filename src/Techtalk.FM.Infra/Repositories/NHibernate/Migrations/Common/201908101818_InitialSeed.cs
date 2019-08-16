using FluentMigrator;
using System;
using Techtalk.FM.Domain.Extensions;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations
{
    [Migration(201908101818)]
    public class InitialSeed : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("admin_user").Row(new
            {
                id = Guid.NewGuid(),
                name = "Felipe Somogyi",
                email = "felipe.somogyi@rakuten.com.br",
                user_password = "12345678".Cript(),
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            });
        }

        public override void Down()
        {
            Delete.FromTable("user");
        }
    }
}
