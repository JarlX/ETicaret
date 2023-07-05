namespace ETicaret.DAL.Concrete.Mapping.BaseMap;

using ETicaretAPI.Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseMap<T> :IEntityTypeConfiguration<T> where T  : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(q => q.ID);  // PK Yapıldı
        builder.Property(q => q.GUID).ValueGeneratedOnAdd(); // Eklendiğinde Değer oluşur
        builder.Property(q => q.ID).ValueGeneratedOnAdd();
    }
}