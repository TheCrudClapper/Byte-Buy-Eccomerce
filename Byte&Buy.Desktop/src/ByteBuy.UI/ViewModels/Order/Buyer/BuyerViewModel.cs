using ByteBuy.Services.DTO.Order;
using ByteBuy.UI.ViewModels.Address;

namespace ByteBuy.UI.ViewModels.Order.Buyer;

public sealed class BuyerViewModel
{
    public string FullName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public HomeAddressViewModel HomeAddress { get; }

    public BuyerViewModel(BuyerSnapshotResponse response)
    {
        FullName = response.FullName;
        Email = response.Email;
        PhoneNumber = $"+48 {response.PhoneNumber}";
        HomeAddress = new HomeAddressViewModel(response.Address);
    }
}
