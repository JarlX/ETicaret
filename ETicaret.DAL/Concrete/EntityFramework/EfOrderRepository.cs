namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using Entity;
using Microsoft.EntityFrameworkCore;

public class EfOrderRepository : EfRepository<Order>,IOrderRepository
{
    public EfOrderRepository(DbContext context) : base(context)
    {
    }
}