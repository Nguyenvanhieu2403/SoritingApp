using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SortingApp_Net.DataContext;
using Z.Dapper.Plus;

namespace SortingApp_Desktop.DataContext.configs
{
    public static class InjectionDataContextExtension
    {
        public static void DepedencyInjectionDatacontext(this IServiceCollection services, IConfiguration configuration)
        {
            DapperPlusManager.Entity<Item>().Table("Item");
            DapperPlusManager.Entity<PostBag>().Table("PostBag");
            DapperPlusManager.Entity<EmployeeShift>().Table("Cms.EmployeeShift");
            DapperPlusManager.Entity<Line>().Table("Cms.Line");
            DapperPlusManager.Entity<PackagingDirectionConfig>().Table("Cms.PackagingDirectionConfig");
            DapperPlusManager.Entity<PackagingDirectionUnit>().Table("Cms.PackagingDirectionUnit");
            DapperPlusManager.Entity<Pos>().Table("Core.POS");
            DapperPlusManager.Entity<ShiftPackaging>().Table("Cms.ShiftPackaging");
            DapperPlusManager.Entity<ShiftConfig>().Table("Cms.ShiftConfig");
            DapperPlusManager.Entity<ShiftItem>().Table("Cms.ShiftItem");
            DapperPlusManager.Entity<Unit>().Table("Core.Unit");
            DapperPlusManager.Entity<Users>().Table("Core.Users");
            DapperPlusManager.Entity<Config>().Table("Cms.Config");
        }
    }
}
