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

            builder.HasMany(u => u.Permissoes)
                .WithOne(p => p.Usuario!)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Denuncias)
                .WithOne(d => d.Usuario!)
                .HasForeignKey(p => p.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuario");
        }
    }
}
