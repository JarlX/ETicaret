namespace ETicaret.DAL.Concrete.Mapping;

using BaseMap;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryMap : BaseMap<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.Property(q => q.CategoryName).HasMaxLength(500).IsRequired();
    }
}