using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace KPIAPI.Data.Models
{
    public class DepEntityTypeConfiguration
        : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("TblDepartmants");
            builder.HasKey(x => x.DepNo);
            builder.Property(x => x.DepNo).IsRequired();
            builder
                .Property(e => e.DepNo)
                .ValueGeneratedNever();
        }
    }
}
