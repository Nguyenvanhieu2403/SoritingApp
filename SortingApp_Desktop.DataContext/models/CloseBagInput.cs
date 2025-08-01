using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.models
{
    public class CloseBagInput
    {
        public string MailNumber { get; set; }
        public string OriginalPost {  get; set; }
        public string DestinationPosCode { get; set; }
        public string BagNumber { get; set; }
        public string MailType { get; set; }
        public int ConfigId { get; set; }
    }
}
