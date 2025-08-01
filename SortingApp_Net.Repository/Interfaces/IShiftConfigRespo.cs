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

namespace SortingApp_Net.Repository.Interfaces
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
