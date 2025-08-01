using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Net.DataContext;

namespace SortingApp_Desktop.Repository.interfaces
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
