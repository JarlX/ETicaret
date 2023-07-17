namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using ETicaretAPI.Entity;
using Microsoft.EntityFrameworkCore;

public class EfProductRepository : EfRepository<Product>,IProductRepository
{
    public EfProductRepository(DbContext context) : base(context)
    {
    }
}