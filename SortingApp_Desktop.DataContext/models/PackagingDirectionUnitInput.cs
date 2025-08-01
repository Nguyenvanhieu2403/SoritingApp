using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.models
{
    public class PackagingDirectionUnitInput
    {
        public long ConfigId { get; set; }
        public HashSet<string> UnitCodeList { get; set; }
    }
}
