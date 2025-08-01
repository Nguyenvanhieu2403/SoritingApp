using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop
{
    public class ApiResponseData
    {
        public int Config_id { get; set; }
        public string Id { get; set; }
        public string Display_name { get; set; }
        public string Name { get; set; }
        public string ModifiedBy { get; set; }
        public string UnitName { get; set; }
        public int TotalPostBag { get; set; }
        public double TotalWeight { get; set; }
        public int TotalScanItem { get; set; }
        public string batchCode { get; set; }
        public int? totalItemOfBatch { get; set; }
        public int? itemBatchIndex { get; set; }
    }
}
