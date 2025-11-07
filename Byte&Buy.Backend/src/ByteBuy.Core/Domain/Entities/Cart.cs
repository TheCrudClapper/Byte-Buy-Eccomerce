using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.Domain.Entities
{
    public class Cart : AuditableEntity, ISoftDelete
    {
        public Guid UserId { get; set; }
        public PortalUser User { get; set; } = null!;
        public Money TotalCartValue { get; set; } = null!;
        public Money TotalItemsValue { get; set; } = null!;
        public ICollection<CartOffer> CartOffers { get; set; } = new List<CartOffer>();
        public bool IsActive { get; set; }
        public DateTime? DateDeleted { get; set; }
        private Cart() { }
    }

    
}
