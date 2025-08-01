using System.Data;
using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.Common.enums;
using SortingApp_Desktop.Common.helpers;
using Z.Dapper.Plus;
using Dapper;

namespace SortingApp_Desktop.Common.@base
{
    /// <summary>
    /// BaseRepos
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class BaseRepos<TModel> : IBaseRepos<TModel> where TModel : class
    {
        /// <summary>
        /// Chuỗi kết nối csql
        /// </summary>
        private readonly string _connectionString;
        /// <summary>
        /// Tên bảng trong CSDL
        /// </summary>
        protected string TableName;
        /// <summary>
        /// Cấu hình có xóa mềm (thay đổi trạng thái dữ liệu)
        /// </summary>
        private readonly bool _useSoftDelete;
        /// <summary>
        /// IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="defaultSchema"></param>
        /// <param name="useSoftDelete"></param>
        protected BaseRepos(IConfiguration configuration, string defaultSchema = "", bool useSoftDelete = true)
        {
            _configuration = configuration;
            _useSoftDelete = useSoftDelete;
            _connectionString = NamingConventionHelpers.GetSqlConnectionString(configuration);
            TableName = string.IsNullOrEmpty(defaultSchema)
                ? typeof(TModel).Name
                : $"[{defaultSchema}].{typeof(TModel).Name}";
        }

        /// <summary>
        /// Tạo kết nối tới CSDL
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            return DbConnectionFactory.GetDbConnection(_connectionString);
        }

        public virtual async Task<MethodResult<IEnumerable<TModel>>> GetsBySearch(BaseSearchModel model)
        {
            var sqlCountQuery = $"SELECT COUNT(1) FROM {TableName} WHERE (1 = 1) ";
            var sqlQuery = $"SELECT * FROM  {TableName} WHERE (1 = 1) ";
            if (model.Status > 0)
            {
                sqlQuery += $" AND (Status = {model.Status} )";
                sqlCountQuery += $" AND (Status = {model.Status} )";
            }
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                sqlQuery += $" AND ( CHARINDEX( '{model.Keyword}', Code) > 0 or CHARINDEX( '{model.Keyword}', Title) > 0 ) ";
                sqlCountQuery += $" AND ( CHARINDEX( '{model.Keyword}', Code) > 0 or CHARINDEX( '{model.Keyword}', Title) > 0 ) ";
            }
            if (model.PageIndex > 0 && model.PageSize > 0)
            {
                sqlQuery += $" ORDER BY {model.OrderCol ?? "Id"} {(model.IsDesc ? "DESC" : "ASC")} " +
                    $" OFFSET {model.PageSize * (model.PageIndex - 1)} ROWS FETCH NEXT {model.PageSize} ROWS ONLY";
            }

            using (var conn = GetOpenConnection())
            {


                //var data = await conn.QueryAsync<TModel>($"{TableName}_Search",
                //    new
                //    {
                //        model.Keyword,
                //        model.Status,
                //        model.PageIndex,
                //        model.PageSize,
                //        model.OrderCol,
                //        model.IsDesc
                //    },
                //    commandType: CommandType.StoredProcedure);
                //var totalRecord = 0;
                //if (model.GetTotalRecord)
                //{
                //    totalRecord = conn.ExecuteScalar<int>($"{TableName}_SearchCount",
                //        new
                //        {
                //            model.Keyword,
                //            model.Status,
                //        },
                //        commandType: CommandType.StoredProcedure);
                //}

                var data = await conn.QueryAsync<TModel>(sqlQuery);
                var totalRecord = 0;
                if (data.Any())
                {
                    totalRecord = conn.ExecuteScalar<int>(sqlCountQuery);
                }
                return MethodResult<IEnumerable<TModel>>.ResultWithData(data, totalRecord: totalRecord);
            }


        }

        public virtual async Task<MethodResult<IEnumerable<TModel>>> GetsAll()
        {
            using (var conn = GetOpenConnection())
            {
                var data = await conn.QueryAsync<TModel>($"SELECT * FROM {TableName} WHERE Status = {(byte)EnumStatus.Used}");
                return MethodResult<IEnumerable<TModel>>.ResultWithData(data);
            }

        }

        public virtual async Task<MethodResult<TModel>> GetById(long id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                var item = await conn.QueryFirstOrDefaultAsync<TModel>(sql, parameters);

                return item == null ? MethodResult<TModel>.ResultWithError("ERROR.NOT_FOUND", message: "ERROR.NOT_FOUND", status: 404) : MethodResult<TModel>.ResultWithData(item);
            }

        }

        public virtual MethodResult<long> Insert(TModel model)
        {
            if (!ValidateInsert(model))
            {
                return MethodResult<long>.ResultWithError("ERROR.VALIDATE_FALSE", 400);
            }
            using (var conn = GetOpenConnection())
            {
                ((IBaseModel)model).Status = (byte)EnumStatus.Used;
                var newItem = conn.BulkInsert(model);

                return MethodResult<long>.ResultWithData(((IBaseModel)newItem.CurrentItem).Id);
            }

        }

        public virtual MethodResult InsertMany(List<TModel> models)
        {
            using (var conn = GetOpenConnection())
            {
                conn.BulkInsert(models);
            }

            return MethodResult.ResultWithSuccess();
        }

        public virtual async Task<MethodResult> Delete(long id, long userId)
        {
            using (var conn = GetOpenConnection())
            {
                if (_useSoftDelete)
                {
                    var sql = $"UPDATE {TableName} set Status = {(byte)EnumStatus.Deleted} WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int64);
                    await conn.ExecuteAsync(sql, parameters);
                }
                else
                {
                    var sql = $"DELETE {TableName} WHERE Id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32);
                    await conn.ExecuteAsync(sql, parameters);
                }
            }
            return MethodResult.ResultWithSuccess();
        }

        public virtual async Task<MethodResult> DeleteMany(string ids, long userId)
        {
            using (var conn = GetOpenConnection())
            {
                if (_useSoftDelete)
                {
                    var sql = $"UPDATE {TableName} set Status = {(short)EnumStatus.Deleted} WHERE Id in ( {ids} )";
                    await conn.ExecuteAsync(sql);
                }
                else
                {
                    var sql = $"DELETE {TableName} WHERE Id in( {ids} )";
                    await conn.ExecuteAsync(sql);
                }


            }
            return MethodResult.ResultWithSuccess();
        }

        public virtual MethodResult Update(TModel model)
        {
            if (!ValidateUpdate(model))
            {
                return MethodResult.ResultWithError("ERROR.VALIDATE_FALSE", "ERROR.VALIDATE_FALSE", 400);
            }
            using (var conn = GetOpenConnection())
            {
                ((IBaseModel)model).Status = ((IBaseModel)model).Status  != null ? ((IBaseModel)model).Status : (byte?)EnumStatus.Used;
                conn.BulkUpdate(model);
                return MethodResult.ResultWithSuccess();

            }

        }

        public virtual bool ValidateInsert(TModel model)
        {
            return true;
        }

        public virtual bool ValidateUpdate(TModel model)
        {
            return true;
        }
    }
}
