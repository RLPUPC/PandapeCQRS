using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pandape.Infrastructure.DataBase;

public class EFRepository<T> : IRepository<T> where T : class
{
    private readonly PandaContext _context;
    private readonly DbSet<T> _dbSet;

    public EFRepository(PandaContext context){
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T Add(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public void Add(IEnumerable<T> entities)
    {
        foreach(T entity in entities) 
        {
            Add(entity);
        }
    }

    public void Delete(T entity)
    {
       _dbSet.Remove(entity);
    }

    public void Delete(IEnumerable<T> entities)
    {
        foreach (T entity in entities) 
        {
            _dbSet.Remove(entity);
        }
    }

    public T? GetById (params object[] keys)
    {
        return _dbSet.Find(keys);
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet;
    }

    public IQueryable<T> Include(params Expression<Func<T, object>>[] expressions)
    {
        IQueryable<T> queryable = _dbSet;
        foreach(Expression<Func<T, object>> expresion in expressions)
        {
            queryable = queryable.Include(expresion);
        }
        return queryable;
    }

    public T Update(T entity)
    {
        return _dbSet.Update(entity).Entity;  
    }

    public void DetachEntity(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }

    public void AttachEntity(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }
}
