namespace ETicaret.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using ETicaretAPI.Entity;

public class OrderManager : IOrderService
{
    public OrderManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Order> GetAsync(Expression<Func<Order, bool>> Filter, params string[] IncludeProperties)
    {
        return await _unitOfWork.OrderRepository.GetAsync(Filter, IncludeProperties);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> Filter = null, params string[] IncludeProperties)
    {
        return await _unitOfWork.OrderRepository.GetAllAsync(Filter, IncludeProperties);
    }

    public async Task<Order> AddSync(Order Entity)
    {
        await _unitOfWork.OrderRepository.AddAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
        return Entity;
    }

    public async Task UpdateAsync(Order Entity)
    {
        await _unitOfWork.OrderRepository.UpdateAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RemoveAsync(Order Entity)
    {
        await _unitOfWork.OrderRepository.RemoveAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }
}