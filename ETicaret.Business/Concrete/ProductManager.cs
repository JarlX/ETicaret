namespace ETicaret.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using ETicaretAPI.Entity;

public class ProductManager : IProductService
{
    public ProductManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> Filter, params string[] IncludeProperties)
    {
        return await _unitOfWork.ProductRepository.GetAsync(Filter, IncludeProperties);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> Filter = null, params string[] IncludeProperties)
    {
        return await _unitOfWork.ProductRepository.GetAllAsync(Filter, IncludeProperties);
    }

    public async Task<Product> AddSync(Product Entity)
    {
        await _unitOfWork.ProductRepository.AddAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
        return Entity;
    }

    public async Task UpdateAsync(Product Entity)
    {
        await _unitOfWork.ProductRepository.UpdateAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RemoveAsync(Product Entity)
    {
        await _unitOfWork.ProductRepository.RemoveAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }
}