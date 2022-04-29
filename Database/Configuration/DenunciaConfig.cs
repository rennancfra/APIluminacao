using Database.Configuration.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class DenunciaConfig : EntityBaseConfig<Denuncia>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Denuncia> builder)
        {
            builder.Property(m => m.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("Denuncia");
        }
    }
}
