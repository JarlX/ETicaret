using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Entity.Base;

namespace ETicaretAPI.Entity;

public class Product : AuditableEntity // İnterface yapmadan AuditableEntity BaseEntity Inherit edip sonrasında Product'ı Audiden inherit ettik
{

    public Product()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }
    public string Name { get; set; }

    public string Description { get; set; }

    public string FeaturedImage { get; set; }

    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}