using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Business.Infraestructure;
using Sat.Recruitment.Data;

namespace Sat.Recruitment.Business
{
    public static class ServiceBusinessExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataServices(configuration);
            
            services.AddScoped<IUserBusiness, UserBusiness>();
        }
    }
}
