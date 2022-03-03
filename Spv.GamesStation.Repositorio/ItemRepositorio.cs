using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spv.GamesStation.Repositorio.Entidades.BD;
using Spv.GamesStation.Repositorio.Interfaz;

namespace Spv.GamesStation.Repositorio
{
    public class ItemRepositorio : RepositorioGenerico<Item>, IItemRepositorio
    {
        public ItemRepositorio(HeroeContext context) : base(context)
        {
        }

        public async Task<bool> ValidarExistenciaItem(int idItem)
        {
            return await DbSet.AnyAsync(x => x.Id == idItem);
        }


    }
}
