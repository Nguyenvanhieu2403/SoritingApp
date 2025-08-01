using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.models
{
    public class ShiftInput
    {
        public string Name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int duration_hours { get; set; }
        public int duration_minute { get; set; }
        public string Username { get; set; }
    }
}
