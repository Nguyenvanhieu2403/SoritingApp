using System.Text.RegularExpressions;

namespace SortingApp_Desktop.Common.utils
{
    public static class StringUtil
    {
        public static string RemoveSqlInjectionChar(string sqlString)
        {
            if (string.IsNullOrEmpty(sqlString))
                return "";

            // Loại bỏ các ký tự đặc biệt
            sqlString = sqlString.Replace("'", "");
            sqlString = sqlString.Replace(";", "");
            sqlString = sqlString.Replace(",", "");
            sqlString = sqlString.Replace("?", "");
            sqlString = sqlString.Replace("<", "");
            sqlString = sqlString.Replace(">", "");
            sqlString = sqlString.Replace("(", "");
            sqlString = sqlString.Replace(")", "");
            sqlString = sqlString.Replace("=", "");
            sqlString = sqlString.Replace("+", "");
            sqlString = sqlString.Replace("*", "");
            sqlString = sqlString.Replace("&", "");
            sqlString = sqlString.Replace("#", "");
            sqlString = sqlString.Replace("%", "");
            sqlString = sqlString.Replace("$", "");

            // Loại bỏ các từ khóa liên quan đến cơ sở dữ liệu
            sqlString = Regex.Replace(sqlString, "select", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "insert", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "delete from", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "count", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "drop table", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "truncate", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "asc", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "mid", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "char", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "exec master", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "net localgroup adminisqlStringators", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "and", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "net user", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "or", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "net", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "-", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "delete", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "drop", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "script", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "update", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "and", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "chr", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "master", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "truncate", "", RegexOptions.IgnoreCase);
            sqlString = Regex.Replace(sqlString, "declare", "", RegexOptions.IgnoreCase);

            return sqlString;
        }
    }
}
