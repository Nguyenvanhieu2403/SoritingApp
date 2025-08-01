using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class ShiftItem
    {
        public string ItemCode { get; set; }
        public long ShiftId { get; set; }
        public string Username { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public long ConfigId { get; set; }
        public string ConfigName { get; set; }
        public string ConfigDisplayName { get; set; }
        public int IsItem { get; set; }
        public long LineId { get; set; }
        public string Bc37Code { get; set; }
        public string BatchCode { get; set; }
    }
}
