using FluentNHibernate.Mapping;
using Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.Infra.Repositories.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("admin_user");

            Id(x => x.Id).Column("id").GeneratedBy.Assigned();

            Map(x => x.Name).Column("name");
            Map(x => x.Email).Column("email");
            Map(x => x.Password).Column("user_password");
            Map(x => x.CreatedAt).Column("created_at").Not.Update();
            Map(x => x.UpdatedAt).Column("updated_at");
        }
    }
}
