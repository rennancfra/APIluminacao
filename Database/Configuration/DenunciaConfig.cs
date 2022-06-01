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

            builder.Property(m => m.UsuarioId)
                .IsRequired();

            builder.Property(m => m.Cep)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(m => m.Numero)
                .IsRequired()
                .HasMaxLength(10);

            builder.ToTable("Denuncia");
        }
    }
}
