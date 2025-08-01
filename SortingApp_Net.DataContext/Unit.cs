using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class Unit
    {
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string ParentUnitCode { get; set; }
        public string UnitTypeCode { get; set; }
    }
}
