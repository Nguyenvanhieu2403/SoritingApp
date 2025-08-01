using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.Common.@base;
using SortingApp_Desktop.DataContext.constants;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.Repository.interfaces;
using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.DataContext.models;
using Dapper;
using Azure;
using System.Data;
using SortingApp_Desktop.DataContext.dtos;

namespace SortingApp_Desktop.Repository
{
    public class BagRepos : BaseRepos<Bag>, IBagReps
    {
        private readonly IConfiguration _configuration;

        public BagRepos(IConfiguration configuration) : base(configuration, ServiceConstant.DefaultSchema)
        {
            _configuration = configuration;
        }

        public async Task<MethodResult<PrintBD8>> CloseBag(CloseBagInput closeBagInput)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@MailNumber", closeBagInput.MailNumber);
                    parameters.Add("@OriginalPost", closeBagInput.OriginalPost);
                    parameters.Add("@DestinationPosCode", closeBagInput.DestinationPosCode.Trim());
                    parameters.Add("@BagNumber", closeBagInput.BagNumber);
                    parameters.Add("@MailType", closeBagInput.MailType);
                    parameters.Add("@ConfigId", closeBagInput.ConfigId);
                    var data = await connection.QueryFirstOrDefaultAsync<PrintBD8>("Cms.UpdateCloseBag", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<PrintBD8>.ResultWithData(data, "", 0);
                }

            }
            catch (Exception ex)
            {
                return MethodResult<PrintBD8>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<Bag>> Create(BagInput bagInput)
        {
            try
            {
                using var conn = GetOpenConnection();
                var item = await conn.QueryFirstOrDefaultAsync<Bag>("Cms.SaveBag", new { ConfigId = bagInput.ConfigId, BagNumber = bagInput.BagNumber, MailNumber = bagInput.MailNumber }, commandType: System.Data.CommandType.StoredProcedure);
                return MethodResult<Bag>.ResultWithData(item, "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<Bag>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<PrintBD8>> PrintBD8(string MailNumber, string BagNumber, string DestinationPosCode)
        {
            try
            {
                using (IDbConnection connection = GetOpenConnection())
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@MailNumber", MailNumber);
                    parameters.Add("@BagNumber", BagNumber);
                    parameters.Add("@DestinationPosCode", DestinationPosCode);
                    var data = await connection.QueryFirstOrDefaultAsync<PrintBD8>("Cms.GetBD8ByMailNumber_BAK", parameters, commandType: CommandType.StoredProcedure);

                    return MethodResult<PrintBD8>.ResultWithData(data, "", 0);
                }

            }
            catch (Exception ex)
            {
                return MethodResult<PrintBD8>.ResultWithError(ex.Message, 400);
            }
        }

        public async Task<MethodResult<List<Bag>>> SearchAsync(int? ProcessId)
        {
            try
            {
                using var conn = GetOpenConnection();
                var item = await conn.QueryAsync<Bag>("Cms.GetAllBag", new { ProcessId = ProcessId }, commandType: System.Data.CommandType.StoredProcedure);
                return MethodResult<List<Bag>>.ResultWithData(item.ToList(), "success", 1);

            }
            catch (Exception ex)
            {
                return MethodResult<List<Bag>>.ResultWithError(ex.Message, 400);
            }
        }
    }
}
