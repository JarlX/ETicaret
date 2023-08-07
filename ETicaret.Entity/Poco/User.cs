using ETicaret.Entity.Base;

namespace ETicaret.Entity;

using Base;

public class User : AuditableEntity
{
    public User()
    {
        this.Orders = new HashSet<Order>();
    }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public virtual IEnumerable<Order> Orders { get; set; }
}