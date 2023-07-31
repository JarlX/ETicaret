namespace ETicaret.Business.Abstract;

using System.Linq.Expressions;

public interface IGenericService<T>
{
    Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeProperties);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeProperties);

    Task<T> AddSync(T Entity);

    Task UpdateAsync(T Entity);

    Task RemoveAsync(T Entity);
}