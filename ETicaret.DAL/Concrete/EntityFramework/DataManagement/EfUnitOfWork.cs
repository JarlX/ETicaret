namespace ETicaret.DAL.Concrete.EntityFramework.DataManagement;

using Abstract;
using Abstract.DataManagement;
using ETicaretAPI.Entity.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class EfUnitOfWork : IUnitOfWork
{

    private readonly ETicaretContext _eTicaretContext;
    private readonly IHttpContextAccessor _httpContextAccessor; 

    public EfUnitOfWork(ETicaretContext eTicaretContext, IHttpContextAccessor httpContextAccessor)
    {
        _eTicaretContext = eTicaretContext;
        _httpContextAccessor = httpContextAccessor;

        CategoryRepository = new EfCategoryRepository(_eTicaretContext);
        UserRepository = new EfUserRepository(_eTicaretContext);
        OrderRepository = new EfOrderRepository(_eTicaretContext);
        ProductRepository = new EfProductRepository(_eTicaretContext);
        OrderDetailRepository = new EfOrderDetailRepository(_eTicaretContext);
    } 
    
    public ICategoryRepository CategoryRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderDetailRepository OrderDetailRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IUserRepository UserRepository { get; }
    public async Task<int> SaveChangeAsync()
    {
        foreach (var item in _eTicaretContext.ChangeTracker.Entries<AuditableEntity>())
        {
            if (item.State == EntityState.Added)
            {
                item.Entity.AddedTime = DateTime.UtcNow;
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.AddedUser = 1;
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.AddedIPV4Address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                item.Entity.UpdatedIPV4Address  = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                if (item.Entity.IsActive == null)
                {
                    item.Entity.IsActive = true;
                }
                item.Entity.IsDeleted = false;
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.UpdatedUser = 1;
                item.Entity.UpdatedIPV4Address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                
            }
        }
        return await _eTicaretContext.SaveChangesAsync();
    }
}