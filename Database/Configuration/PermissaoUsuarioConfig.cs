using Database.Configuration.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class PermissaoUsuarioConfig : EntityBaseConfig<PermissaoUsuario>
    {
        public override void ConfigureEntity(EntityTypeBuilder<PermissaoUsuario> builder)
        {
            builder.Property(p => p.UsuarioID)
                .IsRequired();

            builder.Property(p => p.Permissao)
                .IsRequired();

            builder.ToTable("PermissaoUsuario");
        }
    }
}
