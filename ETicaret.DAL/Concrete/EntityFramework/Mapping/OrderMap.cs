namespace ETicaret.DAL.Concrete.Mapping;

using BaseMap;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderMap : BaseMap<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        builder.HasOne(q => q.User).WithMany(q => q.Orders).HasForeignKey(q => q.UserID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(q => q.OrderDetails).WithOne(q => q.Order).HasForeignKey(q => q.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}