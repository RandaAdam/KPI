using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPIAPI.Data.Models
{
    [Table("TblDepartments")]
    public class Department
    {
        public int DepNo { get; set; }

        public ICollection<KPI>? KPIs { get; set; } = null;
    }
}
