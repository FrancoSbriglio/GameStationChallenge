using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spv.GamesStation.Repositorio.Entidades.BD;

namespace Spv.GamesStation.Repositorio
{
    public sealed class HeroeContext : DbContext
    {
        public HeroeContext(DbContextOptions<HeroeContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Item> Item { get; set; }
        public DbSet<FuerzaGuerrero> FuerzaGuerrero { get; set; }
        public DbSet<Grado> Grado { get; set; }
        public DbSet<Guerrero> Guerrero { get; set; }
        public DbSet<ItemGuerrero> ItemGuerrero { get; set; }
        public DbSet<Nivel> Nivel { get; set; }
        public DbSet<ResistenciaGuerrero> ResistenciaGuerrero { get; set; }
        public DbSet<VelocidadGuerrero> VelocidadGuerrero { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoItem> TipoItem { get; set; }
    }
}
