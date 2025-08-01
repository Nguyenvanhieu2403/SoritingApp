using Dapper;
using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.constants;
using SortingApp_Desktop.DataContext.dtos;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Desktop.Repository.interfaces;
using SortingApp_Net.DataContext;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using Z.Dapper.Plus;

namespace SortingApp_Desktop.Repository
{
    public class EmployeeShiftRepos : BaseRepos<EmployeeShift>, IEmployeeShiftRepos
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectToSql _connectToSql;

        public EmployeeShiftRepos(IConfiguration configuration, ConnectToSql connectToSql) : base(configuration, ServiceConstant.DefaultSchema)
        {
            _configuration = configuration;
            _connectToSql = connectToSql;
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

        public async Task<PackagingDirectionScanDesktop> ScanItem(string itemCode, UserInput input, int processId, bool CheckScanIn, int DividingStage)
        {
            try
            {
                int isItem = 0;
                using (IDbConnection connection = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ItemCode", itemCode);

                    PackagingDirectionScanDesktop packagingDirectionScanDesktop = new PackagingDirectionScanDesktop();
                    var ServiceCode = "";
                    if(processId == 1)
                    {      
                        Boolean isPostBag = true;
                        var countAllByPostBag = await connection.ExecuteScalarAsync<long>("Cms.countAllByPostBagCode", new {itemCode}, commandType: CommandType.StoredProcedure);
                        if (countAllByPostBag == 0) isPostBag = false;
                        List<PostBagCodeAndTotalWeight> mapPosList = new List<PostBagCodeAndTotalWeight>();
                        if (isPostBag)
                        {
                            mapPosList = (await connection.QueryAsync<PostBagCodeAndTotalWeight>("Cms.GetPostBagCodeAndTotalWeight", parameters, commandType: CommandType.StoredProcedure)).ToList();
                            ServiceCode = mapPosList[0]?.ServiceCode;
                        }
                        else
                        {
                            mapPosList.Add(new PostBagCodeAndTotalWeight());
                            var PostCodeByItemCode = await connection.QueryFirstOrDefaultAsync<PostCodeByItemCode>("Cms.getPostCodeByItemCode", new { itemCode }, commandType: CommandType.StoredProcedure);
                            mapPosList[0].posCode = PostCodeByItemCode?.PosCode;
                            ServiceCode = PostCodeByItemCode?.ServiceCode;
                        }
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
                        parameters1.Add("@ProcessId", processId);
                        parameters1.Add("@ServiceCode", ServiceCode);
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
                            TotalItemByBatch batchMap = await connection.QueryFirstOrDefaultAsync<TotalItemByBatch>("Cms.getTotalItemByBatch", parameters2, commandType: CommandType.StoredProcedure);
                            if (batchCode != null && batchCode.Length != 0)
                            {
                                batchCode = batchMap.BatchCode.ToString();
                            }
                        }
                        totalPostBagScan = await SavePostBag(itemCode, input, configList[0], isItem, bc37Code, totalPostBag, totalPostBagScan, batchCode, input.TransferMachine, isPostBag, DividingStage);

                        var parameters3 = new DynamicParameters();
                        parameters3.Add("@batchCode", batchCode);
                        var itemBatchIndex = await connection.ExecuteScalarAsync<long>("Cms.countAllByBatchCode", parameters3, commandType: CommandType.StoredProcedure);

                        if(!CheckScanIn)
                        {
                            var parameters4 = new DynamicParameters();
                            parameters4.Add("@ItemCode", itemCode);
                            await connection.ExecuteScalarAsync("Cms.ScanOutBag", parameters4, commandType: CommandType.StoredProcedure);
                        }

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
                    else
                    {
                        Boolean isPostBag = true;
                        var countAllByPostBag = await connection.ExecuteScalarAsync<long>("Cms.countAllByPostBagCode", new { itemCode }, commandType: CommandType.StoredProcedure);
                        if (countAllByPostBag == 0) isPostBag = false;
                        List<PostBagCodeAndTotalWeight> mapPosList = new List<PostBagCodeAndTotalWeight>();
                        if (isPostBag)
                        {
                            mapPosList = (await connection.QueryAsync<PostBagCodeAndTotalWeight>("Cms.GetPostBagCodeAndTotalWeight", parameters, commandType: CommandType.StoredProcedure)).ToList();
                            ServiceCode = mapPosList[0]?.ServiceCode;
                        }
                        else
                        {
                            mapPosList.Add(new PostBagCodeAndTotalWeight());
                            var PostCodeByItemCode = await connection.QueryFirstOrDefaultAsync<PostCodeByItemCode>("Cms.getProvinceCodeByItemCode", new { itemCode }, commandType: CommandType.StoredProcedure);
                            mapPosList[0].posCode = PostCodeByItemCode?.PosCode;
                            ServiceCode = PostCodeByItemCode?.ServiceCode;
                        }
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
                        parameters1.Add("@ProcessId", processId);
                        parameters1.Add("@ServiceCode", ServiceCode);
                        List<NamePostBagByPosCode> configList = (await connection.QueryAsync<NamePostBagByPosCode>("Cms.getNamePostBagByProvinceCode", parameters1, commandType: CommandType.StoredProcedure)).ToList();
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
                            TotalItemByBatch batchMap = await connection.QueryFirstOrDefaultAsync<TotalItemByBatch>("Cms.getTotalItemByBatch", parameters2, commandType: CommandType.StoredProcedure);
                            if (batchCode != null && batchCode.Length != 0)
                            {
                                batchCode = batchMap.BatchCode.ToString();
                            }
                        }
                        totalPostBagScan = await SavePostBag(itemCode, input, configList[0], isItem, bc37Code, totalPostBag, totalPostBagScan, batchCode, input.TransferMachine, isPostBag, DividingStage);

                        var parameters3 = new DynamicParameters();
                        parameters3.Add("@batchCode", batchCode);
                        var itemBatchIndex = await connection.ExecuteScalarAsync<long>("Cms.countAllByBatchCode", parameters3, commandType: CommandType.StoredProcedure);

                        if (!CheckScanIn)
                        {
                            var parameters4 = new DynamicParameters();
                            parameters4.Add("@ItemCode", itemCode);
                            await connection.ExecuteScalarAsync("Cms.ScanOutBag", parameters4, commandType: CommandType.StoredProcedure);
                        }

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
                           int totalPostBagScan, string batchCode, string transferMachine, Boolean isPostBag, int DividingStage)
        {
            try
            {
                var totalPosBagScan = await UpdateStatusPostBag(itemCode, bc37Code, totalPostBag, totalPostBagScan, transferMachine, isPostBag);
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
                    if(DividingStage == 1)
                    {
                        await connection.QueryAsync("Cms.SaveShiftItem_Bag", parameters1, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        await connection.QueryAsync("Cms.SaveShiftItem", parameters1, commandType: CommandType.StoredProcedure);
                    }
                        return totalPosBagScan;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<int> UpdateStatusPostBag(string postBagCode, string bc37Code, int totalPostBag, int totalPostBagScan, string transferMachine, Boolean isPostBag)
        {
            try
            {
                var PosCode = _configuration.GetSection("PosCode").Value;

                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@postBagCode", postBagCode);
                    parameters.Add("@PosCode", PosCode);
                    var count = await connection.ExecuteScalarAsync<long>("Cms.CountByPostBag", parameters, commandType: CommandType.StoredProcedure);
                    if (count == null || count == 0)
                    {
                        if (transferMachine == null || transferMachine == "") transferMachine = "SortingApp";
                        if(isPostBag)
                        {
                            var parameters1 = new DynamicParameters();
                            parameters1.Add("@postBagCode", postBagCode);
                            await connection.ExecuteAsync("Cms.updateStatusPostBag", parameters1, commandType: CommandType.StoredProcedure);

                            var parameters2 = new DynamicParameters();
                            parameters2.Add("@postBagCode", postBagCode);
                            parameters2.Add("@PosCode", PosCode);
                            parameters2.Add("@transferMachine", transferMachine);
                            await connection.ExecuteAsync("Cms.updateTracePostBag", parameters2, commandType: CommandType.StoredProcedure);

                            await connection.ExecuteAsync("Cms.updateStatusMailTripPostBag", parameters2, commandType: CommandType.StoredProcedure);
                            await connection.ExecuteAsync("Cms.updateTraceItemByPostBag", parameters2, commandType: CommandType.StoredProcedure);
                            await connection.ExecuteAsync("Cms.updateTraceItem", parameters2, commandType: CommandType.StoredProcedure);
                            totalPostBagScan++;
                            if ((bc37Code != null || bc37Code != "") && totalPostBag != 0 && totalPostBag == totalPostBagScan)
                            {
                                var parameters3 = new DynamicParameters();
                                parameters3.Add("@bc37Code", bc37Code);
                                await connection.ExecuteAsync("Cms.updateStatusBC37", parameters3, commandType: CommandType.StoredProcedure);
                            }
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

        public async Task CreateLogScanError(LogScanErrorInput logScanErrorInput)
        {
            try
            {
                var entity = new LogScanError
                {
                    ItemCode = logScanErrorInput.ItemCode,
                    Direction = logScanErrorInput.Direction,
                    ProcessId = logScanErrorInput.ProcessId,
                    CreateDate = DateTime.Now
                };

                using var conn = GetOpenConnection();
                var sql = @"
                    INSERT INTO cms.LogScanError (ItemCode, Direction, ProcessId, CreateDate) 
                    VALUES (@ItemCode, @Direction, @ProcessId, @CreateDate);
                ";
                await conn.ExecuteScalarAsync(sql, entity);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> CheckInMail(string ItemCode)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {
                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@ItemCode", ItemCode);
                    var result = await connection.ExecuteScalarAsync<int>("Cms.CheckItemInMail", parameters1, commandType: CommandType.StoredProcedure);
                    return result == 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
