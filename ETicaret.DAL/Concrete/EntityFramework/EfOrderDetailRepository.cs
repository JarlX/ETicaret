namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using Entity;
using Microsoft.EntityFrameworkCore;

public class EfOrderDetailRepository : EfRepository<OrderDetail>,IOrderDetailRepository
{
    public EfOrderDetailRepository(DbContext context) : base(context)
    {
    }
}