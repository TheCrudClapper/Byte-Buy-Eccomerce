using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class CompanyInfo : AuditableEntity
{
    public string CompanyName { get; set; } = null!;
    public string NIP { get; set; } = null!;
    public AddressValueObj Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
