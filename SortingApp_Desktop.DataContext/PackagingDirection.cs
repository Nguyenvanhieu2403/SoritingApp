using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext
{
    public partial class PackagingDirection
    {
        public long ConfigId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string? ServiceCode { get; set; }
        public int? ProcessId { get; set; }
    }
}
