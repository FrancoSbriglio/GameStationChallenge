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
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(DbContext context) : base(context)
        {
        }

        public async Task<int> DisminuirMonedas(int monedasUsuario, int idUsuario)
        {
            var usuario = await DbSet.FirstAsync(x => x.Id == idUsuario);
            usuario.Monedas = monedasUsuario;
            await Context.SaveChangesAsync();
            return usuario.Monedas;

        }

        public async Task<int> DisminuirDiamantes(int diamantesUsuario, int idUsuario)
        {
            var usuario = await DbSet.FirstAsync(x => x.Id == idUsuario);
            usuario.Diamantes = diamantesUsuario;
            await Context.SaveChangesAsync();
            return usuario.Diamantes;
        }
    }
}
