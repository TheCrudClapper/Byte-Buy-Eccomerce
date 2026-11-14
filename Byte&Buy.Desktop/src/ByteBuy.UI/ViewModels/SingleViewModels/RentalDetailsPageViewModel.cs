using Avalonia.Media.Imaging;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class RentalDetailsPageViewModel : PageViewModel
{
    #region MVVM Props
    [ObservableProperty]
    public string _rentalStatusText = null!;

    [ObservableProperty]
    public string _rentalIcon = null!;

    [ObservableProperty]
    MoneyViewModel _totalPricePaid = null!;

    [ObservableProperty]
    MoneyViewModel _pricePerDay = null!;

    [ObservableProperty]
    ImageThumbnailViewModel _imageThumbnail = null!;

    [ObservableProperty]
    public string _itemName = null!;

    [ObservableProperty]
    public int _quantity;

    [ObservableProperty]
    public int _rentalDays;

    [ObservableProperty]
    public string _borrowerName = null!;

    [ObservableProperty]
    public string _borrowerEmail = null!;

    [ObservableProperty]
    public string _startingRentalDate = null!;

    [ObservableProperty]
    public string _endingRentalDate = null!;

    [ObservableProperty]
    public string _dateCreated = null!;


    #endregion

    private readonly IRentalService _rentalService;
    private readonly IImageService _imageService;
    private readonly INotificationService _notificationService;
    public RentalDetailsPageViewModel(IRentalService rentalService,
        IImageService imageService,
        INotificationService notificationService)
    {
        _rentalService = rentalService;
        _imageService = imageService;
        _notificationService = notificationService;
    }

    public async Task InitializeAsync(Guid rentalId)
    {
        var detailsResult = await _rentalService.GetCompanyRentalAsync(rentalId);
        if (!detailsResult.Success)
        {
            _notificationService.ShowError(detailsResult.Error?.Description ?? "An error occurred while loading the rental.");
            return;
        }

        BuildViewModel(detailsResult.Value);

        await FetchImagePreviewAsync();
    }

    public async void BuildViewModel(RentalLenderResponse dto)
    {
        RentalIcon = RentalMappings.MapRentalStatusIcon(dto.Status);
        RentalStatusText = dto.Status.ToString();
        TotalPricePaid = new MoneyViewModel(dto.TotalPricePaid);
        PricePerDay = new MoneyViewModel(dto.PricePerDay);
        ImageThumbnail = new ImageThumbnailViewModel(dto.Thumbnail.ImagePath);
        ItemName = dto.ItemName;
        Quantity = dto.Quantity;
        RentalDays = dto.RentalDays;
        BorrowerEmail = dto.BorrowerEmail;
        BorrowerName = dto.BorrowerName;
        StartingRentalDate = dto.StartingRentalDate.ToLocalTime().ToString("dd/MM/yyyy");
        DateCreated = dto.DateCreated.ToLocalTime().ToString("dd/MM/yyyy, HH:mm");
        EndingRentalDate = dto.EndingRentalDate.ToLocalTime().ToString("dd/MM/yyyy");
    }

    public async Task FetchImagePreviewAsync()
    {
        var bytes = await _imageService.GetImageBytesAsync(ImageThumbnail.ImagePath);
        if (bytes is null)
            return;

        using var ms = new MemoryStream(bytes);
        var bitmap = new Bitmap(ms);

        ImageThumbnail.Preview = bitmap;
    }
}
