using Microsoft.EntityFrameworkCore;
using Spv.GamesStation.Repositorio.Interfaz;

namespace Spv.GamesStation.Repositorio{

public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
{
    private readonly bool _shareContext;
    protected DbContext Context;

    public RepositorioGenerico(DbContext context)
    {
        Context = context;
        _shareContext = true;
    }

    public DbSet<TEntity> DbSet => Context.Set<TEntity>();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual TEntity Add(TEntity t)
    {
        var newEntry = DbSet.Add(t);
        if (!_shareContext)
            Context.SaveChanges();
        return newEntry.Entity;
    }


    public virtual void SaveChanges()
    {
        Context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_shareContext) Context?.Dispose();
    }
}
}