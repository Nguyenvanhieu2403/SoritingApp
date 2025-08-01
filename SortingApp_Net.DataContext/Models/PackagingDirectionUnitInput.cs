using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext.Models
{
    public class PackagingDirectionUnitInput
    {
        public long ConfigId { get; set; }
        public HashSet<string> UnitCodeList { get; set; }
    }
}
