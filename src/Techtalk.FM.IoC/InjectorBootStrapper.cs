using Microsoft.Extensions.DependencyInjection;

namespace Techtalk.FM.IoC
{
    public class InjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            TechtalkRegister.Register(services);
        }
    }
}
