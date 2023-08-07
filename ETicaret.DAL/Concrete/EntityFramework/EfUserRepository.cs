namespace ETicaret.DAL.Concrete.EntityFramework;

using Abstract;
using DataManagement;
using Entity;
using Microsoft.EntityFrameworkCore;

public class EfUserRepository : EfRepository<User>,IUserRepository
{
    public EfUserRepository(DbContext context) : base(context)
    {
    }
}