using FluentNHibernate.Mapping;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Mappings
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("book");

            Id(x => x.Id).Column("id").GeneratedBy.Assigned();

            Map(x => x.Title).Column("title");
            Map(x => x.Subtitle).Column("subtitle");
            Map(x => x.Author).Column("author");
            Map(x => x.PublishDate).Column("publish_date");
            Map(x => x.Version).Column("version");
            Map(x => x.PageNumber).Column("page_number");
            Map(x => x.PublishingHouse).Column("publishing_house");
            Map(x => x.ISBN).Column("isbn");
            Map(x => x.CreatedAt).Column("created_at").Not.Update();
            Map(x => x.UpdatedAt).Column("updated_at");
        }
    }
}
