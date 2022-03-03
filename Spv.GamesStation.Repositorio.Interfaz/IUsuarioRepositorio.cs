namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IUsuarioRepositorio
    {
        Task<int> DisminuirMonedas(int monedasUsuario, int idUsuario);
        Task<int> DisminuirDiamantes(int diamantesUsuario, int idUsuario);
    }
}