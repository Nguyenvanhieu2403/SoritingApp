using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext.Models
{
    public class PackagingDirectionInput
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Status { get; set; }
        public string ModifiedBy { get; set; }
        public List<string> UnitCodeList { get; set; }
        public long LineId { get; set; }
    }
}
