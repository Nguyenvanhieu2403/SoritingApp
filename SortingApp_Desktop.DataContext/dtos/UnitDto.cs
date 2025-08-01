using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.DataContext.dtos
{
    public class UnitDto
    {
        public string unitCode { get; set; }
        public string unitName { get; set; }
        public string parentUnitCode { get; set; }
        public int isPos { get; set; }
        public List<UnitDto> Children { get; set; } = new List<UnitDto>();

        public void AddChild(UnitDto child)
        {
            Children.Add(child);
        }
    }
}
