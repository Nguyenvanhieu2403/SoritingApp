using System.Linq;

namespace SortingApp_Desktop.Common.extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
