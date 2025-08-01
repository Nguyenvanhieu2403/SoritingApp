using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using VnPostLib.Common.Extensions;

namespace VnPostLib.Common.Helpers
{
    public class NamingConventionHelpers
    {
        /// <summary>
        /// Lấy ConnectionString từ cấu hình
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetSqlConnectionString(IConfiguration configuration)
        {
            var connServer = configuration["ConnectionStrings:Server"];
            var connUserId = configuration["ConnectionStrings:UserID"];
            var connPassword = configuration["ConnectionStrings:Password"];
            var databaseName = configuration["ConnectionStrings:DatabaseName"];
            var connMultipleActiveResultSet = configuration["ConnectionStrings:MultipleActiveResultSets"];

            return $"Server={connServer};Database={databaseName};User ID={connUserId};Password={connPassword};MultipleActiveResultSets={connMultipleActiveResultSet};TrustServerCertificate=True";
        }       

        public static string CombineScopeNameEnv(IConfiguration configuration, string suffix = "")
        {
            var scopeName = GetScopeName(configuration);
            var envName = GetShortEnvironmentName();

            return !string.IsNullOrWhiteSpace(suffix) ? $"{scopeName}_{envName}_{suffix}" : $"{scopeName}_{envName}";
        }

        public static string GetDefaultServiceDatabaseName(IConfiguration configuration)
        {
            var scopeName = GetScopeName(configuration);
            var env = GetShortEnvironmentName();

            var isCore = IsCore(configuration);

            if (isCore)
            {
                return $"{scopeName}_{env}_Core";
            }

            var flattenServiceName = GetRawServiceOrServiceName(configuration).Replace(".", "").ToLower();
            var database = configuration[$"{flattenServiceName}service:ConnectionStrings:DatabaseName"];
            if (!string.IsNullOrWhiteSpace(database))
            {
                return database;
            }

            var serviceName = GetServiceName(configuration);
            serviceName = serviceName.FirstCharToUpper();
            return $"{scopeName}_{env}_{serviceName}";
        }

        public static string GetScopeName(IConfiguration configuration)
        {
            return configuration["Scope:Name"];
        }

        public static string GetOrgCode(IConfiguration configuration)
        {
            return configuration["Scope:OrgCode"];
        }

        public static string GetServiceName(IConfiguration configuration)
        {
            var rawServiceName = configuration["spring:application:rawname"];
            if (!string.IsNullOrWhiteSpace(rawServiceName))
                return rawServiceName;
            return configuration["spring:application:name"];
        }

        public static bool IsCore(IConfiguration configuration)
        {
            var serviceName = GetRawServiceOrServiceName(configuration);

            serviceName = serviceName.Replace(".", "").ToLower();
            if (configuration[$"{serviceName}service:Scope:IsCore"] != null)
            {
                return configuration["Scope:IsCore"] == "true";
            }
            return false;
           
        }

        public static string GetShortEnvironmentName()
        {
            if (EnvironmentHelper.IsTest())
            {
                return "Test";
            }
            else if (EnvironmentHelper.IsStaging())
            {
                return "Stagging";
            }
            else if (EnvironmentHelper.IsProduction())
            {
                return "Prod";
            }

            return "Dev";
        }

        public static string GetFullEnvironmentName()
        {
            if (EnvironmentHelper.IsTest())
            {
                return "Test";
            }
            else if (EnvironmentHelper.IsStaging())
            {
                return "Stagging";
            }
            else if (EnvironmentHelper.IsProduction())
            {
                return "Production";
            }
            else return "Development";
        }

        public static string GetRawServiceOrServiceName(IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("SCOPE__SERVICE") ?? "";
        }
    
    }
}
