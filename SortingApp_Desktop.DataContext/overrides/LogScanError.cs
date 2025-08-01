using SortingApp_Desktop.Common.@base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext
{
    public partial class LogScanError : IBaseModel
    {
        public long Id { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? Modified { get; set; }
        public long? ModifiedBy { get; set; }
    }

}
