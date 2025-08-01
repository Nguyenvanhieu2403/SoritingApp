using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext.Dtos
{
    public class PackagingDirectionDto
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public int status { get; set; }
        public List<string> unitCodeList { get; set; }
    }
}
