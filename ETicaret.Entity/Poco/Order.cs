using ETicaret.Entity.Base;

namespace ETicaret.Entity;

using Base;

public class Order : AuditableEntity
{

    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }
    
    public int UserID { get; set; }

    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

    public virtual User User { get; set; }
}