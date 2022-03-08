using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spv.GamesStation.Repositorio.Entidades.BD;
using Spv.GamesStation.Repositorio.Interfaz;

namespace Spv.GamesStation.Repositorio
{
    public class ItemGuerreroRepositorio : RepositorioGenerico<ItemGuerrero>, IItemGuerreroRepositorio
    {

        public ItemGuerreroRepositorio(HeroeContext context) : base(context)
        {
        }

        public async Task ComprarItemGuerrero(int idItem, int idGuerrero, int tipoItem)
        {
            var guerreroItem = await DbSet.FirstAsync(x => x.IdGuerrero == idGuerrero && x.IdTipoItem == tipoItem);
            guerreroItem.IdItem = idItem;
            await Context.SaveChangesAsync();

        }
    }
}
