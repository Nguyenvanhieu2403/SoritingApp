using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop
{
    public class ConfigureDirectionDto
    {
        public int ConfigId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> UnitCodeList { get; set; }
        public string? ServiceCode { get; set; }
    }
}
