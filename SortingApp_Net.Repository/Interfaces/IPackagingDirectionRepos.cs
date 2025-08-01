using SortingApp_Net.DataContext.Dtos;
using SortingApp_Net.DataContext.Models;
using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Api.Models;
using VnPostLib.Common.Base.Interfaces;
using System.Web.Mvc;

namespace SortingApp_Net.Repository.Interfaces
{
    public interface IPackagingDirectionService : IBaseRepos<PackagingDirectionConfig>
    {
        Task<MethodResult<PackagingDirectionConfig>> SearchAsync(string searchKey, int pageNo, int pageSize, string sortBy, string sortType);

        Task<MethodResult<PackagingDirectionConfig>> CreateAsync(PackagingDirectionInput input);

        Task<MethodResult<PackagingDirectionConfig>> UpdateAsync(PackagingDirectionInput input, long id);

        Task<MethodResult<bool>> DeleteAsync(long id);

        Task<MethodResult<PackagingDirectionDto>> FindByIdAsync(long id);

        Task<MethodResult<bool>> AddUnitAsync(PackagingDirectionUnitInput input);

        Task<MethodResult<List<UnitDto>>> GetTreeUnitAsync();

        Task<MethodResult<List<PackagingDirectionUnit>>> FindAllUnitByConfigIdAsync(long configId);

        Task<MethodResult<string>> GetNamePackagingConfigAsync(string itemCode, UserInput userInput);

        Task<ActionResult> ScanItemAsync(string itemCode, UserInput userInput); // Có thể giữ ActionResult nếu muốn trả về kiểu HTTP cụ thể
    }
}