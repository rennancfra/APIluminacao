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
            builder.Property(u => u.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Senha)
                .HasMaxLength(500);

            builder.Property(u => u.Hash)
                .HasMaxLength(255);

            builder.Property(u => u.Email)
                .HasMaxLength(60);

            builder.Property(u => u.Celular)
                .HasMaxLength(13);

            builder.Property(u => u.Ativo);

            builder.HasMany(u => u.Permissoes)
                .WithOne()
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuario");
        }
    }
}
