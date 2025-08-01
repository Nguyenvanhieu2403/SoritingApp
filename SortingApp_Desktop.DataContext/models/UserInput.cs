using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.models
{
    public class UserInput
    {
        public string Username { get; set; }
        public string Uuid { get; set; }
        public long ShiftId { get; set; }
        public long ShiftConfigId { get; set; }
        public long LineId { get; set; }
        public string TransferMachine { get; set; }
    }
}
