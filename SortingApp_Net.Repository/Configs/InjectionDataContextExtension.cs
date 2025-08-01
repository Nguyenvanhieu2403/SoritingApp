using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SortingApp_Net.DataContext.Configs;
using SortingApp_Net.Repository;
using SortingApp_Net.Repository.Interfaces;

namespace ElectronicProjectManagement.Repository.Configs
{
    public static class InjectionRepositoryExtension
    {
        public static void DependencyInjectionRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.DepedencyInjectionDatacontext(configuration);
            services.AddScoped<IEmployeeShiftRepos, EmployeeShiftRepos>();
        }
    }
}