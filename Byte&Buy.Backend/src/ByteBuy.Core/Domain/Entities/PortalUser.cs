namespace ByteBuy.Core.Domain.Entities;

public class PortalUser : ApplicationUser
{
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public Cart Cart { get; set; } = null!;
}
