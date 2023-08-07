namespace ETicaret.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using ETicaret.DAL.Abstract.DataManagement;
using Entity;

public class CategoryManager : ICategoryService
{
    public CategoryManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Category> GetAsync(Expression<Func<Category, bool>> Filter, params string[] IncludeProperties)
    {
        return await _unitOfWork.CategoryRepository.GetAsync(Filter, IncludeProperties);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> Filter = null, params string[] IncludeProperties)
    {
        return await _unitOfWork.CategoryRepository.GetAllAsync(Filter, IncludeProperties);
    }

    public async Task<Category> AddSync(Category Entity)
    {
        await _unitOfWork.CategoryRepository.AddAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
        return Entity;
    }

    public async Task UpdateAsync(Category Entity)
    {
        await _unitOfWork.CategoryRepository.UpdateAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RemoveAsync(Category Entity)
    {
        await _unitOfWork.CategoryRepository.RemoveAsync(Entity);
        await _unitOfWork.SaveChangeAsync();
    }
}