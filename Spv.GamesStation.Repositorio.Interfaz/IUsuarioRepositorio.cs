using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IUsuarioRepositorio
    {
        Task<int> DisminuirMonedas(int monedasUsuario, int idUsuario);
        Task<int> DisminuirDiamantes(int diamantesUsuario, int idUsuario);
    }
}
