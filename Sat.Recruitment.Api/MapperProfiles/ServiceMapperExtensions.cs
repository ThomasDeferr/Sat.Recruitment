using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.MapperProfiles;
using System;

namespace Sat.Recruitment.Api.MapperProfiles
{
    public static class ServiceMapperExtensions
    {
        public static void AddMapperProfiles(this IServiceCollection services, params Type[] profileAssemblyMarkerTypes)
        {
            services.AddAutoMapper(c =>
            {
                c.AddProfile<UserProfile>();

            }, profileAssemblyMarkerTypes);
        }
    }
}
