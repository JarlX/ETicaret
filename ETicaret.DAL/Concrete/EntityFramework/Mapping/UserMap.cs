namespace ETicaret.DAL.Concrete.Mapping;

using BaseMap;
using ETicaretAPI.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : BaseMap<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property(q => q.FirstName).HasMaxLength(200);
        builder.Property(q=>q.LastName).HasMaxLength(200).IsRequired();
        builder.Property(q=>q.UserName).HasMaxLength(200).IsRequired();
        builder.Property(q => q.Password).HasMaxLength(50).IsRequired();
        builder.Property(q => q.Email).HasMaxLength(100).IsRequired();
        builder.Property(q => q.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(q => q.Adress).IsRequired();
    }
}