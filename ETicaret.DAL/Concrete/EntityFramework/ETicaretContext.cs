using Microsoft.EntityFrameworkCore;

namespace ETicaret.DAL.Concrete.EntityFramework;

public class ETicaretContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer()
        base.OnConfiguring(optionsBuilder);
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer
    // }
}