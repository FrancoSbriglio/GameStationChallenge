namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IItemGuerreroRepositorio
    {
        Task ComprarItemGuerrero(int idItem, int idGuerrero, int tipoItem);
    }
}