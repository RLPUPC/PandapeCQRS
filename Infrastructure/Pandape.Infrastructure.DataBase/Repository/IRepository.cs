using System.Linq.Expressions;

namespace Pandape.Infrastructure.DataBase;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    T? GetById(params object[] keys);
    IQueryable<T> Include(params Expression<Func<T, object>>[] expression);
    T Add(T entity);
    T Update(T entity);
    void DetachEntity(T entity);
    void AttachEntity(T entity);
    void Delete(T entity);
    void Add(IEnumerable<T> entities);       
    void Delete(IEnumerable<T> entities);
}
