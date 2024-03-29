﻿using FluentMigrator;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Migrations.Common
{
    [Migration(201908132204)]
    public class CreateBookTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("book")
                .WithColumn("title").AsString(250).NotNullable()
                .WithColumn("subtitle").AsString(250).NotNullable()
                .WithColumn("author").AsString(150).NotNullable()
                .WithColumn("publish_date").AsDate().NotNullable()
                .WithColumn("version").AsInt16().Nullable().WithDefaultValue(1)
                .WithColumn("page_number").AsInt32().NotNullable()
                .WithColumn("publishing_house").AsString().NotNullable()
                .WithColumn("isbn").AsString(13).Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            IfDatabase("SqlServer", "Postgres")
                .Create.Column("id").OnTable("book").AsGuid().PrimaryKey();

            IfDatabase("Firebird")
                .Create.Column("id").OnTable("book").AsString(38).PrimaryKey();
        }
    }
}
