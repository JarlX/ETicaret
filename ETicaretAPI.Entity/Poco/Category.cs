using ETicaretAPI.Entity.Base;

namespace ETicaretAPI.Entity;

public class Category : AuditableEntity
{
    public Category()
    {
        Products = new HashSet<Product>();
    }
    public string CategoryName  { get; set; }

    public virtual IEnumerable<Product> Products { get; set; }
    
    // EF ---> Lazy Loading / Change Tracking / Proxy
}