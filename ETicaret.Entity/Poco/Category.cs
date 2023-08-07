using ETicaret.Entity.Base;

namespace ETicaret.Entity;

using Base;

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