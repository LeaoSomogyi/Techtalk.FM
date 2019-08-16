using FluentMigrator;
using System;
using Techtalk.FM.Domain.Extensions;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations.PostgreSQL
{
    [Migration(201908101841)]
    [Tags("postgres")]
    public class InsertPostgresUsers : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("admin_user").Row(new
            {
                id = Guid.NewGuid(),
                name = "Postgres User",
                email = "postgres@db.com",
                user_password = "Postgres@2019".Cript(),
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            });
        }

        public override void Down()
        {
            Delete.FromTable("users").Row(new { email = "postgres@db.com" });
        }
    }
}
