using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Base.Interfaces;

namespace SortingApp_Net.DataContext
{
    public partial class ShiftItem : IBaseModel
    {
        public long Id { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? Modified { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
