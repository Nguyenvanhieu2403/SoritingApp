using System;

namespace SortingApp_Desktop.Common.helpers
{
    public class EnvironmentHelper
    {
        private static readonly string EnvironmentKey = "SCOPE__ENVIRONMENT";

        public static bool IsDevelopment()
        {
            return Environment.GetEnvironmentVariable(EnvironmentKey) == "Development";
        }

        public static bool IsProduction()
        {
            return Environment.GetEnvironmentVariable(EnvironmentKey) == "Production";
        }

        public static bool IsStaging()
        {
            return Environment.GetEnvironmentVariable(EnvironmentKey) == "Stagging";
        }

        public static bool IsTest()
        {
            return Environment.GetEnvironmentVariable(EnvironmentKey) == "Test";
        }

        public static bool IsLocal() { 
            return Environment.GetEnvironmentVariable(EnvironmentKey) == "Local";
        }

        public static void SetEnvironment(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
