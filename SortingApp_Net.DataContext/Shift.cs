using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Net.DataContext
{
    public partial class Shift
    {
            [Column("name")]
            public string Name { get; set; }

            [Column("shift_config_id")]
            public long ShiftConfigId { get; set; }

            [Column("start_time")]
            public DateTime StartTime { get; set; }

            [Column("end_time")]
            public DateTime EndTime { get; set; }

            [Column("duration_hours")]
            public int DurationHours { get; set; }

            [Column("duration_minute")]
            public int DurationMinute { get; set; }

            [Column("shift_code")]
            public string ShiftCode { get; set; }

            [Column("created_by")]
            public string CreatedBy { get; set; }

            [Column("created_at")]
            public DateTime CreatedAt { get; set; }

            [Column("updated_by")]
            public string UpdatedBy { get; set; }

            [Column("updated_at")]
            public DateTime UpdatedAt { get; set; }
    }
}
