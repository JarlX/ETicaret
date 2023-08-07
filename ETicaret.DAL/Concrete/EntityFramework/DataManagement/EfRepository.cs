namespace ETicaret.DAL.Concrete.EntityFramework.DataManagement;

using System.Linq.Expressions;
using Abstract.DataManagement;
using Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class EfRepository<T> : IRepository<T> where T : AuditableEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
        
        
    public async Task<T> GetAsync(Expression<Func<T, bool>> Filter, params string[] IncludeProperties)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(Filter);
        if (IncludeProperties.Length > 0)
        {
            foreach (var item in IncludeProperties)
            {
                query.Include(item);
            }
        }

        return await query.SingleOrDefaultAsync();
        
        // return await query.FirstOrDefault -->> Bu kullanılabilir ama aralarındaki fark Eğer dönecek 2 veri varsa hatalı olanıda döndürebilir
        // ama singleordefault hata fırlatır. SOD kullanmak daha mantıklı.
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, params string[] IncludeProperties)
    {
        IQueryable<T> query = _dbSet;

        if (Filter != null)
        {
            query = query.Where(Filter);
            
        }

        if (IncludeProperties.Length > 0)
        {
            foreach (var item in IncludeProperties)
            {
                query.Include(item);
            }
            
        }

        return await Task.Run(() => query);

    }

    public async Task<EntityEntry<T>> AddAsync(T Entity)
    {
        return await _dbSet.AddAsync(Entity);
    }

    public async Task UpdateAsync(T Entity)
    {
        await Task.Run(() => _dbSet.Update(Entity));
    }

    public async Task RemoveAsync(T Entity)
    {
        await Task.Run(() => _dbSet.Remove(Entity));
    }
}