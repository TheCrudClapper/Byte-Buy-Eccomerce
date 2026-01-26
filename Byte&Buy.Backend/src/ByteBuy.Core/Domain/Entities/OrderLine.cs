using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public class OrderLine : AuditableEntity, ISoftDeletable
{
    public Guid OrderId { get; set; }
    public string ItemName { get; set; } = null!;
    public ImageThumbnail Thumbnail { get; set; } = null!;
    public int Quantity { get; set; }
    public bool IsActive {  get; set; }
    public DateTime? DateDeleted { get; set; }

    //Navigation EF
    public Order Order { get; set; } = null!;

}
