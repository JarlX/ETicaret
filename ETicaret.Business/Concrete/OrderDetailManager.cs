namespace ETicaret.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity;

public class OrderDetailManager : IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDetail> GetAsync(Expression<Func<OrderDetail, bool>> Filter, params string[] IncludeProperties)
    {
        return await _unitOfWork.OrderDetailRepository.GetAsync(Filter, IncludeProperties);
    }

    public async Task<IEnumerable<OrderDetail>> GetAllAsync(Expression<Func<OrderDetail, bool>> Filter = null, params string[] IncludeProperties)
    {
        return await _unitOfWork.OrderDetailRepository.GetAllAsync(Filter, IncludeProperties);
    }

    public async Task<OrderDetail> AddSync(OrderDetail Entity)
    {
        await _unitOfWork.OrderDetailRepository.AddAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
        return Entity;
    }

    public async Task UpdateAsync(OrderDetail Entity)
    {
        await _unitOfWork.OrderDetailRepository.UpdateAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RemoveAsync(OrderDetail Entity)
    {
        await _unitOfWork.OrderDetailRepository.RemoveAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }
}