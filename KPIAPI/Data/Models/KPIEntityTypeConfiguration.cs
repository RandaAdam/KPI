using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KPIAPI.Data.Models
{
    public class KPIEntityTypeConfiguration
        : IEntityTypeConfiguration<KPI>
    {
        public void Configure(EntityTypeBuilder<KPI> builder)
        {
            builder.ToTable("TblKPI");
            builder.HasKey(x=>x.KPIIDNum);
            builder.Property(x => x.TargetedValue).IsRequired();
            builder.Property(x=>x.MeasurementUnit).IsRequired(); 
            builder.Property(x=>x.DepNo).IsRequired();


            builder.HasOne(x => x.Dep)
                .WithMany(x => x.KPIs)
                .HasForeignKey(x => x.DepNo);
        }
    }
}
