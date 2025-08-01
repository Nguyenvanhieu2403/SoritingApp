using SortingApp_Net.DataContext;
using SortingApp_Net.DataContext.Dtos;
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
    public interface IEmployeeShiftRepos : IBaseRepos<EmployeeShift>
    {

        Task<MethodResult<List<EmployeeShift>>> Search(string searchKey, DateTime? startDate, DateTime? endDate, int? pageNo, int? pageSize, string sortBy, string sortType);
        Task<MethodResult<EmployeeShift>> SearchByShiftId(long? shiftId, int pageNo, int pageSize, string sortBy, string sortType);
        Task<MethodResult<EmployeeShift>> Create(EmployeeShiftInput employeeShiftInput);
        Task<MemoryStream> ExportExcel(string searchKey, DateTime? startDate, DateTime? endDate);
        Task<PackagingDirectionScanDesktop> ScanItem(string itemCode, UserInput input);
    }
}
