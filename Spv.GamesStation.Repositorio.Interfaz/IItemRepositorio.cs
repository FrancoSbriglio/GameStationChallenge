using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spv.GamesStation.Repositorio.Entidades.BD;

namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IItemRepositorio : IRepositorioGenerico<Item>
    {
        Task<bool> ValidarExistenciaItem(int idItem);
    }
}
