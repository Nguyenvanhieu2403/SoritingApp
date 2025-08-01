using SortingApp_Net.DataContext;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.DataContext.models;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IShiftConfigRespo : IBaseRepos<ShiftConfig>
    {
        Task<MethodResult<Dictionary<string, object>>> Search(string searchKey, int pageNo, int pageSize, string sortBy, string sortType);
        Task<ShiftConfig> Create(ShiftConfigInput input);
        Task<ShiftConfig> Update(ShiftConfigInput input, long id);
        Task Delete(long id);
        Task<Dictionary<string, object>> FindById(long id);
        Task<byte[]> GenerateQRCode(long id, int width, int height);
    }
}
