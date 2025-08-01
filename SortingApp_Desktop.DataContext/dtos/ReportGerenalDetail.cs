using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.dtos
{
    public class ReportGerenalDetail
    {
        public int? ProcessId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ItemCode { get; set; }
        public string Direction { get; set; }
        public string PosCode { get; set; }
    }

}
