using Microsoft.Extensions.DependencyInjection;
using Techtalk.FM.Domain.Contracts.Migrations;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Domain.Contracts.Services;
using Techtalk.FM.Domain.Services;
using Techtalk.FM.Infra.Repositories.NHibernate;
using Techtalk.FM.Infra.Repositories.NHibernate.Migrations;

namespace Techtalk.FM.IoC
{
    public class TechtalkRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMigrationHelper, MigrationHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
