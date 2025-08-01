using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.dtos
{
    public class PackagingDirectionScanDesktop
    {
        public int? TotalPostBag {  get; set; }
        public double? TotalWeight { get; set; }
        public int? TotalScanItem { get; set; }
        public int? TotalItemOfBatch { get; set; }
        public string BatchCode { get; set; }
        public long? ItemBatchIndex { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string unitName { get; set; }
        public string config_id { get; set; }
        public string id { get; set; }
        public string display_name { get; set; }
        public string ModifiedBy { get; set; }
    }
}
