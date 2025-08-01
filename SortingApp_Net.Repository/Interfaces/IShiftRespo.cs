using SortingApp_Net.DataContext.Models;
using SortingApp_Net.DataContext;
using SortingApp_Net.DataContext;
using SortingApp_Net.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Api.Models;
using VnPostLib.Common.Base.Interfaces;
using System.IO;

namespace SortingApp_Net.Repository.Interfaces
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