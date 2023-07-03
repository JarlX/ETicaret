using System.Linq.Expressions;
using ETicaretAPI.Entity.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETicaret.DAL.Abstract.DataManagement;

public interface IRepository<T> where T : AuditableEntity // T --> Generic
{
    Task<T> GetAsync(Expression<Func<T,bool >> Filter, params string[]IncludeParameters ); // Asenkron olduğu için Task kullanıldı / Params en son tipini yazarız

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeParameters);

    Task<EntityEntry<T>> AddAsync(T Entity);

    Task UpdateAsync(T Entity);

    Task RemoveAsync(T Entity);
}
