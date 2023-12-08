using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPIAPI.Data.Models
{
    [Table("TblKPIs")]
    public class KPI
    {
        public int KPIIDNum { get; set; }

        public required string KPIDescription { get; set; }

        //true if the unit is num, fasle if unit is percentage
        public Boolean MeasurementUnit { get; set; }

        public int TargetedValue { get; set; }

        public int DepNo { get; set; }

        public required Department Dep { get; set; }
    }
}
