using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class Employee : ApplicationUser
{
    public AddressValueObj HomeAddress { get; set; } = null!;
    public Guid CompanyInfoId { get; set; }
    public CompanyInfo CompanyInfo { get; set; } = null!;
}
