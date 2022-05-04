namespace Domain.Enums
{
    public enum PermissaoSistemaEnum
    {
        NaoPermitido = -1,
        None = 0,

        // Denuncia
        DenunciaCria = 1,
        DenunciaEdita = 2,
        DenunciaRemove = 3,

        // Usuário Master
        UsuarioMaster = 4,
    }
}
