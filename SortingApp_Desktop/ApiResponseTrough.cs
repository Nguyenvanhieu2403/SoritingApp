using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop
{
    public class ApiResponseTrough
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ApiResponseTroughData> data { get; set; }
        
    }

    public class ApiResponseTroughData 
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Uuid { get; set; }
    }

}
