using System.Linq.Expressions;

namespace APICommons;

public interface IRepository<T> where T : IEntity
{
    public Task<T> GetBy(Expression<Func<T, bool>> predicate);

    public Task<T> GetById(Guid id);

    public Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate, int page);

    public Task<List<T>> GetAll(int page);

    public Task<T> Save(T entity);

    public Task Update(T entity);

    public Task Delete(T entity);
}