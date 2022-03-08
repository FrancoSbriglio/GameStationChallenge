using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Spv.GamesStation.Repositorio.Interfaz
{
    public interface IRepositorioGenerico<TEntity> : IDisposable where TEntity : class
    {
        DbSet<TEntity> DbSet { get; }

        TEntity Add(TEntity t);


        void SaveChanges();
    }
}
