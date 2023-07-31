namespace ETicaret.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using ETicaretAPI.Entity;

public class UserManager : IUserService
{
    public UserManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    public async Task<User> GetAsync(Expression<Func<User, bool>> Filter, params string[] IncludeProperties)
    {
        return await _unitOfWork.UserRepository.GetAsync(Filter, IncludeProperties);
    }

    public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> Filter = null, params string[] IncludeProperties)
    {
        return await _unitOfWork.UserRepository.GetAllAsync(Filter, IncludeProperties);
    }

    public async Task<User> AddSync(User Entity)
    {
        await _unitOfWork.UserRepository.AddAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
        return Entity;
    }

    public async Task UpdateAsync(User Entity)
    {
        await _unitOfWork.UserRepository.UpdateAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RemoveAsync(User Entity)
    {
        await _unitOfWork.UserRepository.RemoveAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }
}