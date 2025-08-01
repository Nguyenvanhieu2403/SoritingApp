using Microsoft.AspNetCore.Mvc;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext.dtos;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Net.DataContext;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IPackagingDirectionService : IBaseRepos<PackagingDirectionConfig>
    {
        Task<MethodResult<List<PackagingDirectionConfig>>> SearchAsync(int? ProcessId, string? ServiceCode);

        Task<MethodResult<PackagingDirectionConfig>> CreateAsync(PackagingDirectionInput input);

        Task<MethodResult<PackagingDirectionConfig>> UpdateAsync(PackagingDirectionInput input, long id);

        Task<MethodResult<bool>> DeleteAsync(long id);

        Task<MethodResult<PackagingDirectionConfig>> FindByIdAsync(long id, int ProcessId);
        Task<MethodResult<PackagingDirectionConfig>> GetByIdCloseBag(long id);

        Task<MethodResult<Pos>> ValidateDestinationPosCode(string DestinationPosCode);
        Task<MethodResult<Pos>> ValidateUnitCode(List<string> ListUnitCode);
        Task<MethodResult<Pos>> GetAllPosAndUnit();

        Task<MethodResult<List<ReportGeneralDto>>> SearchReportGeneralAsync(DateTime FromDate, DateTime ToDate);
        Task<MethodResult<List<ReportGerenalDetail>>> SearchReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate);
        Task<MemoryStream> ExportExcelReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate);
        Task<MemoryStream> ExportExcel1ReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate);
        Task<MethodResult<List<ReportGerenalItem>>> SearchReportItemAsync(string MailNumber, string BagNumber, string ServiceCode);

        Task<MemoryStream> ExportExcelReportItemAsync(string MailNumber, string BagNumber, string ServiceCode);
        Task<MemoryStream> ExportExcel1ReportItemAsync(string MailNumber, string BagNumber, string ServiceCode);


        //Task<MethodResult<bool>> AddUnitAsync(PackagingDirectionUnitInput input);

        //Task<MethodResult<List<UnitDto>>> GetTreeUnitAsync();

        //Task<MethodResult<List<PackagingDirectionUnit>>> FindAllUnitByConfigIdAsync(long configId);

        //Task<MethodResult<string>> GetNamePackagingConfigAsync(string itemCode, UserInput userInput);

        //Task<ActionResult> ScanItemAsync(string itemCode, UserInput userInput); // Có thể giữ ActionResult nếu muốn trả về kiểu HTTP cụ thể
    }
}