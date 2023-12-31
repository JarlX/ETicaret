namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using Entity;
using Microsoft.EntityFrameworkCore;

public class EfCategoryRepository : EfRepository<Category>,ICategoryRepository
{
    public EfCategoryRepository(DbContext context) : base(context)
    {
    }
}