using Database.Configuration.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class MunicipioPrincipalConfig : EntityBaseConfig<MunicipioPrincipal>
    {
        public override void ConfigureEntity(EntityTypeBuilder<MunicipioPrincipal> builder)
        {
            builder.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.UF)
                .IsRequired()
                .HasMaxLength(2);

            builder.ToTable("MunicipioPrincipal");
        }
    }
}
