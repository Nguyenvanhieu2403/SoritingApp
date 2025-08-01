using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.models;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IShiftRespo : IBaseRepos<Shift>
    {
        Task<MethodResult<Dictionary<string, object>>> Search(string searchKey, DateTime date, int pageNo, int pageSize, string sortBy, string sortType);

        Task<MethodResult<Dictionary<string, object>>> SearchReport(string searchKey, DateTime date, int pageNo, int pageSize, string sortBy, string sortType);

        Task<MethodResult<MemoryStream>> ExportExcel(DateTime date);

        Task<MethodResult<Dictionary<string, object>>> FindByUser(long id, string username);

        Task<MethodResult<Shift>> Create(ShiftInput input);

        Task<MethodResult<Shift>> Update(ShiftInput input, long id);

        Task<MethodResult<bool>> Delete(long id);

        Task<MethodResult<Dictionary<string, object>>> FindById(long id);

        Task<MethodResult<byte[]>> GenerateQRCode(long id, int width, int height);

        Task<MethodResult<Dictionary<string, object>>> GetAllItemByShiftId(long shiftId, int pageNo, int pageSize, string sortBy, string sortType);

        Task<MethodResult<Dictionary<string, object>>> GetAllItemByShiftIdAndUsername(long shiftId, string username, int pageNo, int pageSize, string sortBy, string sortType);
    }
}