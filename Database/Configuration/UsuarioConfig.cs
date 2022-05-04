using Database.Configuration.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class UsuarioConfig : EntityBaseConfig<Usuario>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Senha)
                .HasMaxLength(500);

            builder.Property(c => c.Hash)
                .HasMaxLength(255);

            builder.Property(c => c.Email)
                .HasMaxLength(60);

            builder.Property(c => c.Celular)
                .HasMaxLength(13);

            builder.HasMany(c => c.Permissoes)
                .WithOne()
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuario");
        }
    }
}
