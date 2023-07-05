namespace ETicaret.DAL.Concrete.Mapping;

using BaseMap;
using ETicaretAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductMap: BaseMap<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.Property(q => q.Name).HasMaxLength(1000).IsRequired(); 
        builder.HasOne(q => q.Category).WithMany(q => q.Products).HasForeignKey(q=>q.CategoryID).OnDelete(DeleteBehavior.Cascade);
    }
}