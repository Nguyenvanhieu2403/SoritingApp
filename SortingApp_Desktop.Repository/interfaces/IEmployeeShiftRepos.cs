using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext.dtos;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Net.DataContext;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IEmployeeShiftRepos : IBaseRepos<EmployeeShift>
    {

        Task<MethodResult<List<EmployeeShift>>> Search(string searchKey, DateTime? startDate, DateTime? endDate, int? pageNo, int? pageSize, string sortBy, string sortType);
        Task<MethodResult<EmployeeShift>> SearchByShiftId(long? shiftId, int pageNo, int pageSize, string sortBy, string sortType);
        Task<MethodResult<EmployeeShift>> Create(EmployeeShiftInput employeeShiftInput);
        //Task<MemoryStream> ExportExcel(string searchKey, DateTime? startDate, DateTime? endDate);
        Task<PackagingDirectionScanDesktop> ScanItem(string itemCode, UserInput input, int ProcessId, bool CheckScanIn, int DividingStage);
        Task CreateLogScanError(LogScanErrorInput logScanErrorInput);

        Task<bool> CheckInMail(string ItemCode);
    }
}
