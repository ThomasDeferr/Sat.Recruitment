using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Data.Infraestructure;

namespace Sat.Recruitment.Data
{
    public static class ServiceDataExtensions
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserData, UserData>();
        }
    }
}
