using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.Common.@base;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.DataContext.constants;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Desktop.Repository.interfaces;
using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingApp_Desktop.DataContext;
using Dapper;
using System.Diagnostics;
using System.Data;
using SortingApp_Desktop.DataContext.dtos;
using Microsoft.Data.SqlClient;
using SortingApp_Desktop.Common.helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SortingApp_Desktop.Repository
{
    public class PackagingDirectionRepos : BaseRepos<PackagingDirectionConfig>, IPackagingDirectionService
    {
        private readonly IConfiguration _configuration;

        public PackagingDirectionRepos(IConfiguration configuration) : base(configuration, ServiceConstant.DefaultSchema)
        {
            _configuration = configuration;
        }

        public async Task<MethodResult<bool>> DeleteAsync(long id)
        {
            try
            {
                using var conn = GetOpenConnection();

                var sql1 = "DELETE FROM cms.[packaging_direction_unit] WHERE config_Id = @Id";
                await conn.ExecuteAsync(sql1, new { Id = id });

                var sql = "DELETE FROM cms.packaging_direction_config WHERE config_Id = @Id";

                var affectedRows = await conn.ExecuteAsync(sql, new { Id = id });
                conn.Close();

                if (affectedRows > 0)
                {
                    return new MethodResult<bool>
                    {
                        Success = true,
                        Data = true,
                        Message = "Xóa thành công"
                    };
                }
                else
                {
                    return new MethodResult<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = "Không tìm thấy bản ghi với ID này"
                    };
                }
            }
            catch (Exception ex)
            {
                return new MethodResult<bool>
                {
                    Success = false,
                    Data = false,
                    Message = $"Lỗi khi xóa: {ex.Message}"
                };
            }
        }

        private async Task<MethodResult<bool>> CheckExistsAsync(string name, int ProcessId)
        {
            using var conn = GetOpenConnection();
            var count = await conn.QuerySingleAsync<int>(
                $"SELECT COUNT(*) FROM cms.packaging_direction_config WHERE name = '{name}' and ProcessId = {ProcessId}"
            );

            if (count > 0)
            {
                return new MethodResult<bool>
                {
                    Success = true,
                    Message = "Tên hướng đóng đã tồn tại",
                    Data = true
                };
            }

            return new MethodResult<bool>
            {
                Success = false,
                Message = string.Empty,
                Data = false
            };
        }

        private async Task<MethodResult<bool>> CheckExistsUnitAsync(List<string> unitCodeList, long? configId, int ProcessId, string ServiceCode)
        {
            using var conn = GetOpenConnection();

            var sql = $@"
        SELECT COUNT(pdu.config_id)
        FROM cms.packaging_direction_unit pdu
        join cms.packaging_direction_config c on c.config_id = pdu.config_id
        WHERE pdu.unit_code IN @unitCodeList
          AND ( @configId IS NULL OR pdu.Config_Id <> @configId) and c.ProcessId = @ProcessId and c.ServiceCode = '{ServiceCode}' ";

            var count = await conn.QuerySingleAsync<int>(sql, new { unitCodeList, configId, ProcessId = ProcessId });

            if (count > 0)
            {
                return new MethodResult<bool>
                {
                    Success = true,
                    Message = "Đơn vị đã tồn tại",
                    Data = true
                };
            }

            return new MethodResult<bool>
            {
                Success = false,
                Message = string.Empty,
                Data = false
            };
        }



        private async Task CreateUnit(List<string> unitCodeList, long configId)
        {
            using var conn = GetOpenConnection();
            using var tran = conn.BeginTransaction();
            try
            {
                var sql = @"
            INSERT INTO cms.packaging_direction_unit (config_id, unit_code)
            VALUES (@ConfigId, @UnitCode);
        ";

                foreach (var unitCode in unitCodeList)
                {
                    var parameters = new
                    {
                        ConfigId = configId,
                        UnitCode = unitCode
                    };

                    await conn.ExecuteAsync(sql, parameters, transaction: tran);
                }

                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

        public async Task DeleteAllByConfigId(long id)
        {
            using var conn = GetOpenConnection();
            var sql = "DELETE FROM cms.packaging_direction_unit WHERE config_Id = @Id";
            await conn.ExecuteAsync(sql, new { Id = id });
            conn.Close();
        }

        public async Task<MethodResult<PackagingDirectionConfig>> CreateAsync(PackagingDirectionInput packingInput)
        {
            var nameResult = await CheckExistsAsync(packingInput.Name, packingInput.ProcessId.Value);
            if (nameResult.Data)
            {
                return new MethodResult<PackagingDirectionConfig>
                {
                    Success = false,
                    Message = nameResult.Message,
                    Data = null
                };
            }

            var unitResult = await CheckExistsUnitAsync(packingInput.UnitCodeList, null, packingInput.ProcessId.Value, packingInput.ServiceCode);
            if (unitResult.Data)
            {
                return new MethodResult<PackagingDirectionConfig>
                {
                    Success = false,
                    Message = unitResult.Message,
                    Data = null
                };
            }

            try
            {
                var entity = new PackagingDirectionConfig
                {
                    Name = packingInput.Name,
                    DisplayName = packingInput.DisplayName,
                    Status = packingInput.Status,
                    ServiceCode = packingInput.ServiceCode,
                    ProcessId = packingInput.ProcessId,
                    DestinationPosCode = packingInput.DestinationPosCode,
                    PTVC = packingInput.PTVC,
                };

                using var conn = GetOpenConnection();
                var sql = @"
            INSERT INTO cms.packaging_direction_config (Name, Display_Name, Status, ServiceCode, ProcessId, DestinationPosCode, PTVC) 
            VALUES (@Name, @DisplayName, @Status, @ServiceCode, @ProcessId, @DestinationPosCode, @PTVC);
            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);
        ";
                entity.ConfigId = await conn.QuerySingleAsync<long>(sql, entity);
                await CreateUnit(packingInput.UnitCodeList, entity.ConfigId);
                return new MethodResult<PackagingDirectionConfig>
                {
                    Success = true,
                    Message = "Create success",
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return MethodResult<PackagingDirectionConfig>.ResultWithError(ex.Message, 400);

            }
        }

        public async Task<MethodResult<PackagingDirectionConfig>> UpdateAsync(PackagingDirectionInput packingInput, long id)
        {
            try
            {
                using var conn = GetOpenConnection();
                var sql = "SELECT [config_id] ,[display_name] ,[name],[ServiceCode] ,[ProcessId] FROM cms.packaging_direction_config WHERE config_Id = @id";
                var item = await conn.QueryFirstOrDefaultAsync<PackagingDirection>(sql, new { id });
                if (item.Name != packingInput.Name.Trim())
                {
                    await CheckExistsAsync(packingInput.Name.Trim(), packingInput.ProcessId.Value);
                }
                await CheckExistsUnitAsync(packingInput.UnitCodeList, id, packingInput.ProcessId.Value, packingInput.ServiceCode);
                var sqlUpdate = @"UPDATE cms.packaging_direction_config
                                SET 
                                    Name = @Name,
                                    Display_Name = @DisplayName, 
                                    Status = @Status,
                                    ServiceCode = @ServiceCode,
                                    DestinationPosCode = @DestinationPosCode,
                                    PTVC = @PTVC
                                WHERE config_Id = @ConfigId ";
                int affectedRows = await conn.ExecuteAsync(sqlUpdate, new
                {
                    ConfigId = id,
                    Name = packingInput.Name,
                    DisplayName = packingInput.DisplayName,
                    Status = packingInput.Status,
                    ServiceCode = packingInput.ServiceCode,
                    DestinationPosCode = packingInput.DestinationPosCode,
                    PTVC = packingInput.PTVC
                });

                if (affectedRows > 0)
                {
                    await DeleteAllByConfigId(id);
                    await CreateUnit(packingInput.UnitCodeList, id);
                }

                return new MethodResult<PackagingDirectionConfig>
                {
                    Success = true,
                    Message = "Update success"
                };
            }
            catch (Exception ex)
            {
                return MethodResult<PackagingDirectionConfig>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<List<PackagingDirectionConfig>>> SearchAsync(int? ProcessId, string? ServiceCode)
        {
            try
            {
                using var conn = GetOpenConnection();
                var condition = $"where ProcessId = {ProcessId}";

                if(!string.IsNullOrEmpty(ServiceCode) )
                {
                    condition += $" and ServiceCode = '{ServiceCode}'";
                }

                var sql = $@"SELECT distinct
                            c.config_id AS ConfigId,
                            c.display_name AS DisplayName,
                            c.name AS Name,
                            c.ServiceCode,
                            c.ProcessId,
                            c.PTVC,
                            ISNULL(agg.TotalItemWeight / 1000, 0) AS TotalItemWeight,
                            ISNULL(agg.ItemCount, 0) AS CountItem,
                            ISNULL(unit.UnitCodeList, '') AS UnitCodeList,
                            c.DestinationPosCode
                           FROM [VnPostSoftingApp].[Cms].[packaging_direction_config] c
                            LEFT JOIN (
                                SELECT distinct
                                    si.config_id,
                                    si.item_code,
                                    COUNT(*) AS ItemCount,
                                    SUM(b.ItemWeight) AS TotalItemWeight
                                FROM VnPostSoftingApp.Cms.shift_item si
                                INNER JOIN VnPostSoftingApp.Cms.bag b 
                                    ON si.BagNumber = b.BagNumber and si.ServiceCode = b.ServiceCode
                                join [VnPostSoftingApp].[Cms].[packaging_direction_config]  c1 on c1.config_id = si.config_id
                                {condition} and si.StatusBCCP IS NULL AND si.BagNumber IS NOT NULL  
                                GROUP BY si.config_id, si.item_code
                            ) agg ON c.config_id = agg.config_id
                            LEFT JOIN (
                                SELECT 
                                    u2.config_id,
                                    STUFF((
										SELECT ',' + u3.unit_code
										FROM VnPostSoftingApp.Cms.packaging_direction_unit u3
										WHERE u3.config_id = u2.config_id
										FOR XML PATH(''), TYPE
									).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS UnitCodeList
                                FROM VnPostSoftingApp.Cms.packaging_direction_unit u2
                                GROUP BY u2.config_id
                            ) unit ON c.config_id = unit.config_id
                            {condition}
                            ORDER BY c.config_id DESC";

                var item = await conn.QueryAsync<PackagingDirectionConfig>(sql);
                return MethodResult<List<PackagingDirectionConfig>>.ResultWithData(item.ToList(), "success",1);

            }
            catch (Exception ex)
            {
                return MethodResult<List<PackagingDirectionConfig>>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<PackagingDirectionConfig>> FindByIdAsync(long id, int ProcessId)
        {
            try
            {
                using var conn = GetOpenConnection();
                var sql = @"SELECT 
                                c.config_id  as ConfigId,
                                c.display_name as DisplayName,
                                c.name as Name,
                                c.serviceCode as ServiceCode,
                                c.ProcessId as ProcessId,
                                c.DestinationPosCode as DestinationPosCode,
                                c.PTVC as PTVC, 
                                STUFF((
                                    SELECT ',' + u2.unit_code
                                    FROM VnPostSoftingApp.Cms.packaging_direction_unit u2
                                    WHERE u2.config_id = c.config_id
                                    and c.ProcessId = @ProcessId
                                    FOR XML PATH(''), TYPE
                                ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS UnitCodeList
                            FROM 
                                VnPostSoftingApp.Cms.packaging_direction_config c
                            LEFT JOIN 
                                VnPostSoftingApp.Cms.packaging_direction_unit u
                                ON c.config_id = u.config_id
                            where c.config_id = @ConfigId
                            and c.ProcessId = @ProcessId
                            GROUP BY 
                                c.ProcessId, c.serviceCode, c.config_id, c.display_name, c.name, c.DestinationPosCode, c.PTVC";

                var item = await conn.QueryFirstOrDefaultAsync<PackagingDirectionConfig>(sql, new { ConfigId = id, ProcessId = ProcessId });
                return MethodResult<PackagingDirectionConfig>.ResultWithData(item, "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<PackagingDirectionConfig>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<PackagingDirectionConfig>> GetByIdCloseBag(long id)
        {
            try
            {
                using var conn = GetOpenConnection();
                var item = await conn.QueryFirstOrDefaultAsync<PackagingDirectionConfig>("Cms.GetByIdCloseBag", new { ConfigId = id}, commandType: System.Data.CommandType.StoredProcedure);
                return MethodResult<PackagingDirectionConfig>.ResultWithData(item, "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<PackagingDirectionConfig>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<Pos>> ValidateDestinationPosCode(string DestinationPosCode)
        {
            try
            {
                using var conn = GetOpenConnection();
                var item = await conn.QueryFirstOrDefaultAsync<Pos>("Cms.ValidateDestinationPosCode", new { DestinationPosCode = DestinationPosCode }, commandType: System.Data.CommandType.StoredProcedure);
                return MethodResult<Pos>.ResultWithData(item, "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<Pos>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<Pos>> ValidateUnitCode(List<string> ListUnitCode)
        {
            try
            {
                using var conn = GetOpenConnection();

                var ListPosAndUnit = await conn.QueryAsync<Pos>("Cms.GetAllPosAndUnit", commandType: System.Data.CommandType.StoredProcedure);

                var isCheck = ListUnitCode.All(unitCode => ListPosAndUnit.Any(pos => pos.POSCode?.Trim() == unitCode?.Trim()));

                if (!isCheck)
                {
                    return MethodResult<Pos>.ResultWithError("Mã đơn vị không hợp lệ", 400);
                }

                //var item = await conn.QueryFirstOrDefaultAsync<Pos>("Cms.ValidateUnitCode", new { DestinationPosCode = ListUnitCode }, commandType: System.Data.CommandType.StoredProcedure);
                return MethodResult<Pos>.ResultWithData(null, "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<Pos>.ResultWithError(ex.Message, 400);
            }
        }

        public Task<MethodResult<Pos>> GetAllPosAndUnit()
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResult<List<ReportGeneralDto>>> SearchReportGeneralAsync(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@FromDate", FromDate);
                    parameters.Add("@ToDate", ToDate);
                    var data = await connection.QueryAsync<ReportGeneralDto>("Cms.GetAllReportGeneral", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<List<ReportGeneralDto>>.ResultWithData(data.ToList(), "", 0);
                }

            }
            catch (Exception ex)
            {
                return MethodResult<List<ReportGeneralDto>>.ResultWithError(ex.Message, 400);
            }
        }

        private async Task<DataTable> ExportExcelToDataTable(int ProcessId, DateTime FromDate, DateTime ToDate)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(NamingConventionHelpers.GetSqlConnectionString(_configuration)))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("Cms.GetAllReportGeneralDetail", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProcessId", ProcessId);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@ToDate", ToDate);
                    command.CommandTimeout = 420;
                    using (SqlDataAdapter adapter1 = new SqlDataAdapter(command))
                    {
                        adapter1.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }


        public async Task<MemoryStream> ExportExcelReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate)
        {
            var exportFile = new MemoryStream();

            #region call list api
            var result = await ExportExcelToDataTable(ProcessId, FromDate, ToDate);
            #endregion

            #region xuất excel từ dữ liệu thuần
            var writer = new StreamWriter(exportFile, System.Text.Encoding.UTF8);

            // Ghi tiêu đề cột
            List<string> columnHeaders = new List<string>
            {
                "Mã bưu gửi",
                "Thời gian quét",
                "Hướng đóng",
                "Bưu cục phát"
            };

            for (int i = 0; i < result.Columns.Count; i++)
            {
                writer.Write(columnHeaders[i]);
                if (i < result.Columns.Count - 1) writer.Write(",");
            }
            writer.WriteLine();

            // Ghi dữ liệu
            foreach (DataRow row in result.Rows)
            {
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    writer.Write(EscapeCsvValue(row[i]?.ToString()));
                    if (i < result.Columns.Count - 1) writer.Write(",");
                }
                writer.WriteLine();
            }
            writer.Flush();
            exportFile.Position = 0;
            return exportFile;
            #endregion
        }

        // Hàm hỗ trợ để thoát giá trị CSV (xử lý dấu phẩy và ký tự đặc biệt)
        private string EscapeCsvValue(string value)
        {
            if (value == null) return "";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                value = value.Replace("\"", "\"\"");
                return "\"" + value + "\"";
            }
            return value;
        }

        public async Task<MethodResult<List<ReportGerenalDetail>>> SearchReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@ProcessId", ProcessId);
                    parameters.Add("@FromDate", FromDate);
                    parameters.Add("@ToDate", ToDate);
                    var data = await connection.QueryAsync<ReportGerenalDetail>("Cms.GetAllReportGeneralDetail", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<List<ReportGerenalDetail>>.ResultWithData(data.ToList(), "", 0);
                }

            }
            catch (Exception ex)
            {
                return MethodResult<List<ReportGerenalDetail>>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MemoryStream> ExportExcel1ReportGeneralDetailAsync(int ProcessId, DateTime FromDate, DateTime ToDate)
        {
            var exportFile = new MemoryStream();

            #region call list api
            var result = await ExportExcelToDataTable(ProcessId, FromDate, ToDate);
            #endregion

            var writer = new StreamWriter(exportFile, System.Text.Encoding.UTF8);

            // Ghi mở đầu XML (Excel định dạng XML Spreadsheet)
            writer.WriteLine(@"<?xml version=""1.0""?>");
            writer.WriteLine(@"<?mso-application progid=""Excel.Sheet""?>");
            writer.WriteLine(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
                          xmlns:o=""urn:schemas-microsoft-com:office:office""
                          xmlns:x=""urn:schemas-microsoft-com:office:excel""
                          xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">");
            writer.WriteLine(@"<Worksheet ss:Name=""BaoCaoChiTiet"">");
            writer.WriteLine("<Table>");

            // Header cố định hoặc lấy từ DataTable
            List<string> columnHeaders = new List<string>
            {
                "Mã bưu gửi",
                "Thời gian quét",
                "Hướng đóng",
                "Bưu cục phát"
            };

            // Ghi dòng tiêu đề
            writer.WriteLine("<Row>");
            foreach (var header in columnHeaders)
            {
                writer.WriteLine($@"<Cell><Data ss:Type=""String"">{EscapeXml(header)}</Data></Cell>");
            }
            writer.WriteLine("</Row>");

            // Ghi dữ liệu
            foreach (DataRow row in result.Rows)
            {
                writer.WriteLine("<Row>");
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    string value = row[i]?.ToString() ?? "";
                    writer.WriteLine($@"<Cell><Data ss:Type=""String"">{EscapeXml(value)}</Data></Cell>");
                }
                writer.WriteLine("</Row>");
            }

            // Đóng Workbook
            writer.WriteLine("</Table>");
            writer.WriteLine("</Worksheet>");
            writer.WriteLine("</Workbook>");

            writer.Flush();
            exportFile.Position = 0;
            return exportFile;
        }

        private string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return System.Security.SecurityElement.Escape(input);
        }



        public async Task<MethodResult<List<ReportGerenalItem>>> SearchReportItemAsync(string MailNumber, string BagNumber, string ServiceCode)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@MailNumber", MailNumber);
                    parameters.Add("@BagNumber", BagNumber);
                    parameters.Add("@ServiceCode", ServiceCode);
                    var data = await connection.QueryAsync<ReportGerenalItem>("Cms.GetAllReportItem", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<List<ReportGerenalItem>>.ResultWithData(data.ToList(), "", 0);
                }

            }
            catch (Exception ex)
            {
                return MethodResult<List<ReportGerenalItem>>.ResultWithError(ex.Message, 400);
            }
        }

        private async Task<DataTable> ExportExcelReportItemToDataTable(string MailNumber, string BagNumber, string ServiceCode)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(NamingConventionHelpers.GetSqlConnectionString(_configuration)))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("Cms.GetAllReportItem", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MailNumber", MailNumber);
                    command.Parameters.AddWithValue("@BagNumber", BagNumber);
                    command.Parameters.AddWithValue("@ServiceCode", ServiceCode);
                    command.CommandTimeout = 420;
                    using (SqlDataAdapter adapter1 = new SqlDataAdapter(command))
                    {
                        adapter1.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        public async Task<MemoryStream> ExportExcelReportItemAsync(string MailNumber, string BagNumber, string ServiceCode)
        {
            var exportFile = new MemoryStream();

            #region call list api
            var result = await ExportExcelReportItemToDataTable(MailNumber, BagNumber, ServiceCode);
            #endregion

            #region xuất excel từ dữ liệu thuần
            var writer = new StreamWriter(exportFile, System.Text.Encoding.UTF8);

            // Ghi tiêu đề cột
            List<string> columnHeaders = new List<string>
            {
                "Số túi",
                "Mã bưu gửi",
                "Dịch vụ",
                "Khối lượng",
                "Bưu cục phát"
            };

            for (int i = 0; i < result.Columns.Count; i++)
            {
                writer.Write(columnHeaders[i]);
                if (i < result.Columns.Count - 1) writer.Write(",");
            }
            writer.WriteLine();

            // Ghi dữ liệu
            foreach (DataRow row in result.Rows)
            {
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    writer.Write(EscapeCsvValue(row[i]?.ToString()));
                    if (i < result.Columns.Count - 1) writer.Write(",");
                }
                writer.WriteLine();
            }
            writer.Flush();
            exportFile.Position = 0;
            return exportFile;
            #endregion
        }

        public async Task<MemoryStream> ExportExcel1ReportItemAsync(string MailNumber, string BagNumber, string ServiceCode)
        {
            var exportFile = new MemoryStream();

            #region call list api
            var result = await ExportExcelReportItemToDataTable(MailNumber, BagNumber, ServiceCode);
            #endregion

            #region xuất Excel XML thủ công
            var writer = new StreamWriter(exportFile, System.Text.Encoding.UTF8);

            writer.WriteLine(@"<?xml version=""1.0""?>");
            writer.WriteLine(@"<?mso-application progid=""Excel.Sheet""?>");
            writer.WriteLine(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
                          xmlns:o=""urn:schemas-microsoft-com:office:office""
                          xmlns:x=""urn:schemas-microsoft-com:office:excel""
                          xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">");

            writer.WriteLine(@"<Worksheet ss:Name=""BaoCaoBuuGui"">");
            writer.WriteLine("<Table>");

            // Tiêu đề cột
            List<string> columnHeaders = new List<string>
    {
        "Số túi",
        "Mã bưu gửi",
        "Dịch vụ",
        "Khối lượng",
        "Bưu cục phát"
    };

            writer.WriteLine("<Row>");
            foreach (var header in columnHeaders)
            {
                writer.WriteLine($@"<Cell><Data ss:Type=""String"">{EscapeXml(header)}</Data></Cell>");
            }
            writer.WriteLine("</Row>");

            // Ghi dữ liệu
            foreach (DataRow row in result.Rows)
            {
                writer.WriteLine("<Row>");
                for (int i = 0; i < result.Columns.Count; i++)
                {
                    var raw = row[i]?.ToString() ?? "";
                    writer.WriteLine($@"<Cell><Data ss:Type=""String"">{EscapeXml(raw)}</Data></Cell>");
                }
                writer.WriteLine("</Row>");
            }

            writer.WriteLine("</Table>");
            writer.WriteLine("</Worksheet>");
            writer.WriteLine("</Workbook>");

            writer.Flush();
            exportFile.Position = 0;
            return exportFile;
            #endregion
        }

    }
}
