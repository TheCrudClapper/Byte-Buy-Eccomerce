using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities;

public abstract class OrderLine : AuditableEntity, ISoftDeletable
{
    public Guid OrderId { get; private set; }
    public Guid OfferId { get; private set; }
    public string ItemName { get; private set; } = null!;
    public ImageThumbnail Thumbnail { get; private set; } = null!;
    public abstract Money TotalPrice { get; }
    public int Quantity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    //Navigation EF
    public Order Order { get; private set; } = null!;

    protected OrderLine() { }

    protected OrderLine(Guid orderId, Guid offerId, string itemName, ImageThumbnail thumbnail, int quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        OfferId = offerId;
        ItemName = itemName;
        Thumbnail = thumbnail;
        Quantity = quantity;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
