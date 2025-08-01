using SortingApp_Net.DataContext;
using SortingApp_Net.DataContext.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Api.Models;
using VnPostLib.Common.Base.Interfaces;

namespace SortingApp_Net.Repository.Interfaces
{
    public interface ILineRepos : IBaseRepos<Line>
    {
        Task<MethodResult<Line>> SearchAsync(string searchKey, int pageNo, int pageSize, string sortBy, string sortType);

        Task<Line> CreateAsync(LineInput input);

        Task<Line> UpdateAsync(LineInput input, long id);

        Task DeleteAsync(long id);

        Task<Line> FindByIdAsync(long id);

        Task<MethodResult<Dictionary<string, object>>> ReportAsync(DateTime? fromDate, DateTime? toDate, long? lineId, int pageNo, int pageSize, string sortBy, string sortType);

        Task<List<Line>> FindAllAsync(string uuid);

        Task CleanLineActiveAsync(string uuid);

        Task<MemoryStream> ExportReportLineAsync(DateTime? fromDate, DateTime? toDate, long? lineId);
    }
}
