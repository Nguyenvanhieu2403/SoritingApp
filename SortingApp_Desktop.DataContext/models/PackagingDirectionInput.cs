using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.models
{
    public class PackagingDirectionInput
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public byte? Status { get; set; }
        public string ModifiedBy { get; set; }
        public List<string> UnitCodeList { get; set; }
        public long LineId { get; set; }
        public string? ServiceCode { get; set; }
        public int? ProcessId { get; set; }
        public string? DestinationPosCode { get; set; }
        public string? PTVC { get; set; }
    }
}
