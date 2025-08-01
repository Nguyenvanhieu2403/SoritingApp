using Azure;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using SortingApp_Net.DataContext;
using SortingApp_Net.DataContext.Constants;
using SortingApp_Net.DataContext.Dtos;
using SortingApp_Net.DataContext.Models;
using SortingApp_Net.DataContext.Overrides;
using SortingApp_Net.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Api.Models;
using VnPostLib.Common.Base;
using VnPostLib.Common.Base.Interfaces;
using VnPostLib.Common.Helpers;
using Z.Dapper.Plus;

namespace SortingApp_Net.Repository
{
    public class EmployeeShiftRepos : BaseRepos<EmployeeShift>, IEmployeeShiftRepos
    {
        private readonly IConfiguration _configuration;

        public EmployeeShiftRepos(IConfiguration configuration) : base(configuration, ServiceConstant.DefaultSchema)
        {
            _configuration = configuration;
        }

        public async Task<MethodResult<EmployeeShift>> Create(EmployeeShiftInput employeeShiftInput)
        {
            try
            {
                EmployeeShift data = new EmployeeShift();
                data.ShiftId = long.Parse(employeeShiftInput.ShiftId);
                data.Username = employeeShiftInput.Username;
                data.CreateDate = DateTime.Now;
                data.EmpName = "";
                data.PhoneNumber = "";
                data.StartTime = DateTime.Now;
                using (IDbConnection connection = GetOpenConnection())
                {
                    await connection.SingleInsertAsync(data);

                }
                return null;

            }
            catch (Exception ex)
            {
                return MethodResult<EmployeeShift>.ResultWithError(ex.Message, 400);

            }

        }
        private async Task<List<Dictionary<string, object>>> GetAllExcelList(string searchKey, DateTime? startDate, DateTime? endDate)
        {
            string startDateStr = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            string endDateStr = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;

            var result = new List<Dictionary<string, object>>();

            using (SqlConnection conn = new SqlConnection(NamingConventionHelpers.GetSqlConnectionString(_configuration)))
            {
                await conn.OpenAsync();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"
                SELECT es.id AS id, 
                es.username AS username, 
                es.shiftId AS shiftId,
                es.startTime AS startTime, 
                es.empName AS empName,
                es.phoneNumber AS phoneNumber,
                s.name AS name, 
                s.startTime AS startTimeShift, 
                s.endTime AS endTimeShift, 
                COUNT(DISTINCT si.itemCode) AS output 
                FROM EmployeeShift es 
                JOIN Shift s ON s.id = es.shiftId 
                LEFT JOIN ShiftItem si ON si.username = es.username AND si.shiftId = es.shiftId 
                WHERE (@searchKey IS NULL OR @searchKey = '' OR LOWER(es.empName) LIKE CONCAT('%',LOWER(@searchKey),'%')) 
                AND (@startDate IS NULL OR es.startTime >= @startDate) 
                AND (@endDate IS NULL OR @endDate >= es.startTime) 
                GROUP BY es.id,es.username,es.shiftId,es.startTime,es.empName,es.phoneNumber,s.name,s.startTime,s.endTime";

                    command.Parameters.AddWithValue("@searchKey", string.IsNullOrEmpty(searchKey) ? (object)DBNull.Value : searchKey);
                    command.Parameters.AddWithValue("@startDate", string.IsNullOrEmpty(startDateStr) ? (object)DBNull.Value : startDateStr);
                    command.Parameters.AddWithValue("@endDate", string.IsNullOrEmpty(endDateStr) ? (object)DBNull.Value : endDateStr);

                    command.CommandTimeout = 400;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }

        public async Task<MemoryStream> ExportExcel(string searchKey, DateTime? startDate, DateTime? endDate)
        {
            using (var package = new ExcelPackage())
            {


                var sheet = package.Workbook.Worksheets.Add("Data");

                string[] headers = { "STT", "Tài khoản", "Họ tên", "Số điện thoại", "Sản lượng", "Giờ vào ca", "Tên ca", "Thời gian bắt đầu", "Thời gian kết thúc" };
                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.Cells[1, i + 1].Value = headers[i];
                    sheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                var listMap = await GetAllExcelList(searchKey, startDate, endDate);

                int rowIdx = 2;
                foreach (var map in listMap)
                {
                    sheet.Cells[rowIdx, 1].Value = rowIdx - 1;
                    sheet.Cells[rowIdx, 2].Value = map["username"]?.ToString();
                    sheet.Cells[rowIdx, 3].Value = map["empName"]?.ToString();
                    sheet.Cells[rowIdx, 4].Value = map["phoneNumber"]?.ToString();
                    sheet.Cells[rowIdx, 5].Value = map["output"]?.ToString();

                    if (map["startTime"] is DateTime startTime)
                        sheet.Cells[rowIdx, 6].Value = startTime;

                    sheet.Cells[rowIdx, 7].Value = map["name"]?.ToString();

                    if (map["startTimeShift"] is DateTime startTimeShift)
                        sheet.Cells[rowIdx, 8].Value = startTimeShift;

                    if (map["endTimeShift"] is DateTime endTimeShift)
                        sheet.Cells[rowIdx, 9].Value = endTimeShift;

                    rowIdx++;
                }
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                return stream;
            }
        }

        public async Task<MethodResult<List<EmployeeShift>>> Search(string searchKey, DateTime? startDate, DateTime? endDate, int? pageNo, int? pageSize, string sortBy, string sortType)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@searchKey", searchKey ?? "");
                    parameters.Add("@startDate", startDate);
                    parameters.Add("@endDate", endDate);
                    parameters.Add("@pageNo", pageNo ?? 1);
                    parameters.Add("@pageSize", pageSize ?? 20);
                    var data = await connection.QueryAsync<EmployeeShift>("Cms.EmployeeShiftGetBySearch", parameters, commandType: CommandType.StoredProcedure);

                    int totalRecord = await connection.ExecuteScalarAsync<int>("Cms.EmployeeShiftGetBySearch_Count", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<List<EmployeeShift>>.ResultWithData(data.ToList(), "", totalRecord);
                }
                    
            }
            catch (Exception ex)
            {
                return MethodResult<List<EmployeeShift>>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<EmployeeShift>> SearchByShiftId(long? shiftId, int pageNo, int pageSize, string sortBy, string sortType)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {
                    var query = @"
                SELECT 
                es.id AS id,
                es.username AS username,
                es.shiftId AS shiftId,
                es.startTime AS startTime,
                es.empName AS empName,
                es.phoneNumber AS phoneNumber,
                COUNT(si.itemCode) AS output
                FROM EmployeeShift es 
                LEFT JOIN ShiftItem si ON si.username = es.username AND si.shiftId = es.shiftId
                WHERE es.shiftId = @shiftId
                GROUP BY es.id, es.username, es.shiftId, es.startTime, es.empName, es.phoneNumber";

                    var parameters = new DynamicParameters();
                    parameters.Add("@shiftId", shiftId);
                    parameters.Add("@offset", (pageNo - 1) * pageSize);
                    parameters.Add("@pageSize", pageSize);
                    EmployeeShift items = connection.QueryAsync(query, parameters).Result.FirstOrDefault();
                    return MethodResult<EmployeeShift>.ResultWithData(items, "Success", 1);
                }

                    
            }
            catch (Exception ex)
            {
                return MethodResult<EmployeeShift>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<PackagingDirectionScanDesktop> ScanItem(string itemCode, UserInput input)
        {
            try
            {
                int isItem = 0;
                using (IDbConnection connection = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ItemCode", itemCode);

                    PackagingDirectionScanDesktop packagingDirectionScanDesktop = new PackagingDirectionScanDesktop();


                    List<PostBagCodeAndTotalWeight> mapPosList = (await connection.QueryAsync<PostBagCodeAndTotalWeight>("Cms.GetPostBagCodeAndTotalWeight", parameters, commandType: CommandType.StoredProcedure)).ToList();
                    if (mapPosList == null || mapPosList.Count == 0)
                    {
                        packagingDirectionScanDesktop.Message = "Không tồn tại";
                    }

                    var mapPos = mapPosList[0];
                    string posCode = mapPos.posCode;
                    string bc37Code = mapPos.bc37Code;

                    int totalPostBag = mapPos.totalPostBag;

                    int totalPostBagScan = mapPos.totalPostBagScan;

                    double totalWeight = mapPos.totalWeight;

                    if (string.IsNullOrEmpty(posCode))
                    {
                        packagingDirectionScanDesktop.Message = "Không tồn tại";
                        packagingDirectionScanDesktop.Name = "-1";
                        packagingDirectionScanDesktop.TotalPostBag = null;
                        packagingDirectionScanDesktop.TotalWeight = null;
                        packagingDirectionScanDesktop.TotalScanItem = null;
                        packagingDirectionScanDesktop.TotalItemOfBatch = null;
                        packagingDirectionScanDesktop.BatchCode = null;
                        packagingDirectionScanDesktop.ItemBatchIndex = null;
                        return packagingDirectionScanDesktop;
                    }
                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@PosCode", posCode);
                    List<NamePostBagByPosCode> configList = (await connection.QueryAsync<NamePostBagByPosCode>("Cms.getNamePostBagByPosCode", parameters1, commandType: CommandType.StoredProcedure)).ToList();
                    if (configList.Count == 0)
                    {
                        packagingDirectionScanDesktop.Message = "Chưa có hướng đóng";
                        packagingDirectionScanDesktop.Name = "-1";
                        packagingDirectionScanDesktop.TotalPostBag = null;
                        packagingDirectionScanDesktop.TotalWeight = null;
                        packagingDirectionScanDesktop.TotalScanItem = null;
                        packagingDirectionScanDesktop.TotalItemOfBatch = null;
                        packagingDirectionScanDesktop.BatchCode = null;
                        packagingDirectionScanDesktop.ItemBatchIndex = null;
                        return packagingDirectionScanDesktop;
                    }
                    var shiftId = await GetShiftId(input.ShiftConfigId, input.Username);
                    input.ShiftId = shiftId;
                    string batchCode = null;
                    if (itemCode.Length == 13)
                    {
                        var parameters2 = new DynamicParameters();
                        parameters2.Add("@itemCode", itemCode);
                        Dictionary<string, object> batchMap = (await connection.QueryFirstOrDefaultAsync<Dictionary<string, object>>("Cms.getTotalItemByBatch", parameters2, commandType: CommandType.StoredProcedure));
                        if (batchCode != null && batchCode.Length != 0)
                        {
                            batchCode = batchMap["batchCode"].ToString();
                        }
                    }
                    totalPostBagScan = await SavePostBag(itemCode, input, configList[0], isItem, bc37Code, totalPostBag, totalPostBagScan, batchCode, input.TransferMachine);

                    var parameters3 = new DynamicParameters();
                    parameters3.Add("@batchCode", batchCode);
                    var itemBatchIndex = (await connection.ExecuteScalarAsync<long>("Cms.countAllByBatchCode", parameters3, commandType: CommandType.StoredProcedure));

                    packagingDirectionScanDesktop.TotalPostBag = totalPostBag;
                    packagingDirectionScanDesktop.TotalWeight = totalWeight;
                    packagingDirectionScanDesktop.TotalScanItem = totalPostBagScan;
                    packagingDirectionScanDesktop.TotalItemOfBatch = totalPostBagScan;
                    packagingDirectionScanDesktop.BatchCode = batchCode;
                    packagingDirectionScanDesktop.ItemBatchIndex = itemBatchIndex;
                    packagingDirectionScanDesktop.Name = configList[0].name.ToString();
                    packagingDirectionScanDesktop.ModifiedBy = input.Username.ToString();
                    packagingDirectionScanDesktop.unitName = configList[0].unitName.ToString();
                    packagingDirectionScanDesktop.config_id = configList[0].config_id.ToString();
                    packagingDirectionScanDesktop.id = configList[0].id.ToString();
                    packagingDirectionScanDesktop.display_name = configList[0].display_name.ToString();
                    return packagingDirectionScanDesktop;
                }
            }
            catch (Exception ex)
            {
                PackagingDirectionScanDesktop packagingDirectionScanDesktop = new PackagingDirectionScanDesktop();
                packagingDirectionScanDesktop.Name = "-1";
                packagingDirectionScanDesktop.TotalPostBag = null;
                packagingDirectionScanDesktop.TotalWeight = null;
                packagingDirectionScanDesktop.TotalScanItem = null;
                packagingDirectionScanDesktop.TotalItemOfBatch = null;
                packagingDirectionScanDesktop.BatchCode = null;
                packagingDirectionScanDesktop.ItemBatchIndex = null;
                packagingDirectionScanDesktop.Message = ex.Message;
                return packagingDirectionScanDesktop;
            }
        }

        private async Task<long> GetShiftId(long shiftConfigId, string username)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@username", username);
                    var user = await connection.QueryFirstOrDefaultAsync<Users>("Cms.FindByUsername", parameters, commandType: CommandType.StoredProcedure);

                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@shiftConfigId", shiftConfigId);
                    var shiftConfig = await connection.QueryFirstOrDefaultAsync<ShiftConfig>("Cms.findShiftConfigById", parameters1, commandType: CommandType.StoredProcedure);

                    var parameters2 = new DynamicParameters();
                    parameters2.Add("@shiftConfigId", shiftConfigId);
                    parameters2.Add("@startTime", shiftConfig.StartTime);
                    parameters2.Add("@endTime", shiftConfig.EndTime);
                    var shiftOpt = await connection.QueryAsync<Shift>("Cms.findAllByShiftConfigIdAndStartTimeAndEndTime", parameters2, commandType: CommandType.StoredProcedure);
                    if (shiftOpt != null)
                    {
                        var parameters3 = new DynamicParameters();
                        parameters3.Add("@shiftConfigId", shiftConfigId);
                        parameters3.Add("@ShiftCode", "280425");
                        parameters3.Add("@startTime", shiftConfig.StartTime);
                        parameters3.Add("@endTime", shiftConfig.EndTime);
                        parameters3.Add("@DurationHours", shiftConfig.DurationHours);
                        parameters3.Add("@DurationMinute", shiftConfig.DurationMinute);
                        parameters3.Add("@Name", shiftConfig.Name);
                        parameters3.Add("@CreatedAt", DateTime.Now);
                        parameters3.Add("@CreatedBy", shiftConfig.CreateBy.ToString());
                        parameters3.Add("@UserName", username);
                        var ShiftPackagingIdNew = await connection.ExecuteScalarAsync<long>("Cms.SaveShiftPackaging", parameters3, commandType: CommandType.StoredProcedure);
                        return ShiftPackagingIdNew;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<int> SavePostBag(string itemCode, UserInput input, NamePostBagByPosCode config, int isItem, string bc37Code, int totalPostBag,
                           int totalPostBagScan, string batchCode, string transferMachine)
        {
            try
            {
                var totalPosBagScan = await UpdateStatusPostBag(itemCode, bc37Code, totalPostBag, totalPostBagScan, transferMachine);
                long configId = config.config_id;
                string configName = config.name.ToString();
                string displayName = config.display_name;
                string unitCode = config.id.ToString();
                string unitName = config.unitName;
                using(IDbConnection connection = GetOpenConnection())
                {
                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@shiftId", input.ShiftId);
                    parameters1.Add("@itemCode", itemCode);
                    parameters1.Add("@isItem", isItem);
                    parameters1.Add("@unitCode", unitCode);
                    parameters1.Add("@unitName", unitName);
                    parameters1.Add("@configId", configId);
                    parameters1.Add("@configName", configName);
                    parameters1.Add("@displayName", displayName);
                    parameters1.Add("@lineId", input.LineId);
                    parameters1.Add("@bc37Code", bc37Code);
                    parameters1.Add("@batchCode", batchCode);
                    await connection.QueryAsync("Cms.SaveShiftItem", parameters1, commandType: CommandType.StoredProcedure);
                    return totalPosBagScan;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<int> UpdateStatusPostBag(string postBagCode, string bc37Code, int totalPostBag, int totalPostBagScan, string transferMachine)
        {
            try
            {
                string appPath = @"C:\SortingApp";
                string configFilePath = Path.Combine(appPath, "config.txt");

                if (!File.Exists(configFilePath))
                {
                    return 0;
                }

                string[] configLines = File.ReadAllLines(configFilePath);
                var PosCode = configLines.Length > 0 ? configLines[0].Trim() : "";

                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@postBagCode", postBagCode);
                    parameters.Add("@PosCode", PosCode);
                    var count = await connection.ExecuteScalarAsync<long>("Cms.CountByPostBag", parameters, commandType: CommandType.StoredProcedure);
                    if (count == null || count == 0)
                    {
                        if (transferMachine == null || transferMachine == "") transferMachine = "SortingApp";
                        var parameters1 = new DynamicParameters();
                        parameters1.Add("@postBagCode", postBagCode);
                        await connection.QueryAsync("Cms.updateStatusPostBag", parameters1, commandType: CommandType.StoredProcedure);

                        var parameters2 = new DynamicParameters();
                        parameters2.Add("@postBagCode", postBagCode);
                        parameters2.Add("@PosCode", PosCode);
                        parameters2.Add("@transferMachine", transferMachine);
                        await connection.QueryAsync("Cms.updateTracePostBag", parameters2, commandType: CommandType.StoredProcedure);

                        await connection.QueryAsync("Cms.updateStatusMailTripPostBag", parameters2, commandType: CommandType.StoredProcedure);
                        await connection.QueryAsync("Cms.updateTraceItemByPostBag", parameters2, commandType: CommandType.StoredProcedure);
                        await connection.QueryAsync("Cms.updateTraceItem", parameters2, commandType: CommandType.StoredProcedure);
                        totalPostBagScan++;
                        if ((bc37Code != null || bc37Code != "") && totalPostBag != 0 && totalPostBag == totalPostBagScan)
                        {
                            var parameters3 = new DynamicParameters();
                            parameters3.Add("@bc37Code", bc37Code);
                            await connection.QueryAsync("Cms.updateStatusBC37", parameters3, commandType: CommandType.StoredProcedure);
                        }
                    }
                }

                return totalPostBagScan;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
