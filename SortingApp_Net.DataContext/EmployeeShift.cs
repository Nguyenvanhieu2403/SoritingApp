using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class EmployeeShift
    {
        public long ShiftId { get; set; }
        public string Username { get; set; }
        public string EmpName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Output { get; set; }
    }
}
