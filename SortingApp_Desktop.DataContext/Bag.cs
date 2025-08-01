using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext
{
    public partial class Bag
    {
        public string? BagNumber { get; set; }
        public string? MailNumber { get; set; }
        public string? ItemCode { get; set; }
        public Decimal? BagWeight { get; set; }
        public Decimal? ItemWeight { get; set; }
        public Decimal? ShellWeight { get; set; }
        public int? CountItem { get; set; }
        public string? BD8 { get; set; }
        public string? ServiceCode { get; set; }
        public string? DestinationPosCode { get; set; }
    }
}
