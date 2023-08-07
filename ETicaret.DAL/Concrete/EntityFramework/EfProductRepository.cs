namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using Entity;
using Microsoft.EntityFrameworkCore;

public class EfProductRepository : EfRepository<Product>,IProductRepository
{
    public EfProductRepository(DbContext context) : base(context)
    {
    }
}