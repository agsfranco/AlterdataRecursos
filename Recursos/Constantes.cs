namespace Recursos
{
    public static class Constantes
    {
        public const int LoginTtl = 20; //Em Minutos

        public const string LogadoStatus = "logadoStatus";
        public const string UsuarioId = "usuarioId";
        public const string UsuarioNome = "usuarioNome";
        public const string UrlLogin = "~/Login.aspx";

        public const string PaginaRequisitada = "paginaRequisitada";

        public const string PaginaInicial = "~/InicialRecursos.aspx";

        public enum StatusLogin { logado = 1, naoLogado = 0 };// 1-logado  0-nao logado

        public enum StatusVotacao { proposto = 1, em_desenvolvimento = 2, concluido = 3, cancelado = 4 };
    }
}