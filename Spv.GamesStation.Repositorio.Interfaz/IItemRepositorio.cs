using Spv.GamesStation.Repositorio.Entidades.BD;

namespace Spv.GamesStation.Repositorio.Interfaz{

public interface IItemRepositorio: IRepositorioGenerico<Item>
{
    Task<bool> ValidarExistenciaItem(int idItem);
}
}