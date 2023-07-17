using Microsoft.EntityFrameworkCore;

namespace ETicaret.DAL.Concrete.EntityFramework;

using Mapping;

public class ETicaretContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            
            optionsBuilder.UseSqlServer(
                "Data Source=localhost;Initial Catalog=ShoppingApp;User ID=SA;Password=reallyStrongPwd123;TrustServerCertificate=true;");
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new OrderDetailMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        
        base.OnModelCreating(modelBuilder);
    }
}
    