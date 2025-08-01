using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class PackagingDirectionUnit
    {
        public long UnitId { get; set; }
        public long ConfigId { get; set; }
        public string UnitCode { get; set; }
        public string ParentCode { get; set; }
        public int IsDisplay { get; set; }
        public int IsPos { get; set; }
        public int Priority { get; set; }
    }
}
