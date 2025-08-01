using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SortingApp_Desktop.DataContext.configs;
using SortingApp_Desktop.Repository.interfaces;

namespace SortingApp_Desktop.Repository.configs
{
    public static class InjectionRepositoryExtension
    {
        public static void DependencyInjectionRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.DepedencyInjectionDatacontext(configuration);
            services.AddScoped<IEmployeeShiftRepos, EmployeeShiftRepos>();
            services.AddScoped<IConfigRepos, ConfigRepos>();
        }
    }
}