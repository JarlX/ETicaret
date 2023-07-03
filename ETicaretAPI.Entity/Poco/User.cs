using ETicaretAPI.Entity.Base;

namespace ETicaretAPI.Entity;

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

    public string Adress { get; set; }

    public virtual IEnumerable<Order> Orders { get; set; }
}