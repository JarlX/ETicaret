namespace ETicaret.DAL.Abstract.DataManagement;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    
    IOrderRepository OrderRepository { get; }
    
    IOrderDetailRepository OrderDetailRepository { get; }
    
    IProductRepository ProductRepository { get; }
    
    IUserRepository UserRepository { get; }
    
    Task<int> SaveChangeAsync();
} 