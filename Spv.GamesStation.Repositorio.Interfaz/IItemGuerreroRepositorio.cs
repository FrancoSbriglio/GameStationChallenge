using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IItemGuerreroRepositorio
    {
        Task ComprarItemGuerrero(int idItem, int idGuerrero, int tipoItem);
    }
}
