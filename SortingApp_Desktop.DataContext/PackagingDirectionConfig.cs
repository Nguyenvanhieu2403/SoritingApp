using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class PackagingDirectionConfig
    {
        public long ConfigId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string? UnitCodeList { get; set; }
        public string? ServiceCode { get; set; }
        public int? ProcessId { get; set; }
        public int? CountItem { get; set; }
        public float? TotalWeight { get; set; }
        public string? BagNumber { get; set; }
        public string? MailNumber { get; set; }
        public string? OriginalPost { get; set; }
        public string? DestinationPosCode { get; set; }
        public float? TotalItemWeight { get; set; }
        public string? PTVC { get; set; }
    }
}
