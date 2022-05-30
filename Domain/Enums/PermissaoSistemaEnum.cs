namespace Domain.Enums
{
    public enum PermissaoSistemaEnum
    {
        NaoPermitido = -1,
        None = 0,

        // Denuncia
        DenunciaCria = 1,
        DenunciaEdita = 2,

        // Usuário Master // NUNCA MUDAR!!!
        UsuarioMaster = 3,

        // Usuario
        UsuarioAtiva = 4,
        UsuarioCria = 5,
    }
}
