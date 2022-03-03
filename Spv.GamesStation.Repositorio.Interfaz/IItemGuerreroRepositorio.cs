using System.Threading.Tasks;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.InputDto;

namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IItemGuerreroRepositorio
    {
        Task ComprarItemGuerrero(int idItem, int idGuerrero, int tipoItem);
    }
}