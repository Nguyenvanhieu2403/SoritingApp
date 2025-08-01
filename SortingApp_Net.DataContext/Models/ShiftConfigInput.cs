using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext.Models
{
    public class ShiftConfigInput
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinute { get; set; }
        public string UnitCodeList { get; set; }
    }
}
