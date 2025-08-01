using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.dtos
{
    public class PostBagCodeAndTotalWeight
    {
        public string bc37Code { get; set; }
        public string posCode { get; set; }
        public int totalPostBag { get; set; }
        public int totalPostBagScan { get; set; }
        public double totalWeight { get; set; }
        public string ServiceCode { get; set; }

    }
}
