namespace ByteBuy.Core.DTO.Offer.Enum;

//Discriminator enum that helps while mapping dtos to polymorphic ones in memory
public enum OfferType
{
    Sale = 0,
    Rent = 1
}
