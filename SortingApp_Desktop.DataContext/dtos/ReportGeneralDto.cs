using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.dtos
{
    public class ReportGeneralDto
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int CountTotalItem { get; set; }
        public int CountItemSuccess { get; set; }
        public int CountItemError { get; set; }
        public float SuccessRate { get; set; }
    }

}
