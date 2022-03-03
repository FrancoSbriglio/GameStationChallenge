using Microsoft.EntityFrameworkCore;

namespace Spv.GamesStation.Repositorio.Interfaz{

public interface IRepositorioGenerico<TEntity> : IDisposable where TEntity : class
{
    DbSet<TEntity> DbSet { get; }

    TEntity Add(TEntity t);


    void SaveChanges();
}
}