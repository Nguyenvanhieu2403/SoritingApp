using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop
{
    public class ApiResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public ApiResponseData data { get; set; }
    }
}
