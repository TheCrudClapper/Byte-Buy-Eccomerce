using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects
{
    public class AddressValueObj
    {
        public string Street { get; private set; } = null!;
        public string HouseNumber { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string? FlatNumber { get; private set; }

        private AddressValueObj() { }

        private AddressValueObj(
            string street,
            string houseNumber,
            string postalCode,
            string city,
            string country,
            string? flatNumber = null)
        {
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
            FlatNumber = flatNumber;
        }

        public override string ToString()
            => $"{Street} {HouseNumber}{(FlatNumber != null ? $"/{FlatNumber}" : "")}, {PostalCode} {City}, {Country}";

        public static Result<AddressValueObj> Create(string street, string houseNumber,string postalCode, string city, string country, string? flatNumber = null)
        {
            //walidation
            return new AddressValueObj(street, houseNumber, postalCode, city, country, flatNumber);
        }

        public static AddressValueObj FromEntity(Entities.Address address)
            => new AddressValueObj(
                address.Street,
                address.HouseNumber,
                address.PostalCode,
                address.City,
                address.Country.Name,
                address.FlatNumber
            );
    }
}

