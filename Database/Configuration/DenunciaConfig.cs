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

            builder.Property(m => m.UsuarioID)
                .IsRequired();

            builder.Property(m => m.CEP)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(m => m.Numero)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasOne(d => d.Usuario)
                .WithMany(u => u!.Denuncias)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Denuncia");
        }
    }
}
