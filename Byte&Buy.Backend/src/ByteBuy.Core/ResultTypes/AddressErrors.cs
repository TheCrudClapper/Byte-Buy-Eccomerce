namespace ByteBuy.Core.ResultTypes;

public static class AddressErrors
{
    public static readonly Error NoDefaultAddress = new(
        ErrorType.NotFound,
        "ShippingAddres.Default",
        "No default address found for this user, courier deliveries are not avaliable.");

    public static readonly Error StreetInvalid = Error.Validation(
      "Address.Street",
      "Street is required and must be at most 50 characters.");

    public static readonly Error HouseNumberInvalid = Error.Validation(
        "Address.HouseNumber",
        "House number is required and must be at most 20 characters.");

    public static readonly Error PostalCodeInvalid = Error.Validation(
        "Address.PostalCode",
        "Postal code is required and must be at most 20 characters.");

    public static readonly Error PostalCityInvalid = Error.Validation(
        "Address.PostalCity",
        "Postal city is required and must be at most 50 characters.");

    public static readonly Error CityInvalid = Error.Validation(
        "Address.City",
        "City is required and must be at most 50 characters.");

    public static readonly Error CountryInvalid = Error.Validation(
        "Address.Country",
        "Country is required and must be at most 50 characters.");

    public static readonly Error FlatNumberInvalid = Error.Validation(
        "Address.FlatNumber",
        "Flat number must be at most 10 characters.");
}
