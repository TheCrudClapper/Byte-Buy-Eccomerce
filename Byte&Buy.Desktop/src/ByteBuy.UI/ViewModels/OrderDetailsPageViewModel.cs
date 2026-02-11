using Avalonia.Media.Imaging;
using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.DTO.Order.Enums;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Order.Buyer;
using ByteBuy.UI.ViewModels.Order.OrderDelivery;
using ByteBuy.UI.ViewModels.Order.OrderLine;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExCSS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace ByteBuy.UI.ViewModels;

public partial class OrderDetailsPageViewModel : PageViewModel
{
    #region MVVM Fields

    [ObservableProperty]
    public Guid _orderId;

    [ObservableProperty]
    public string _purchasedDate = null!;

    [ObservableProperty]
    public string _sellerDisplayName = null!;

    [ObservableProperty]
    public int _linesCount;

    [ObservableProperty]
    OrderStatus _status;

    [ObservableProperty]
    public MoneyViewModel _totalItemsCost = null!;

    [ObservableProperty]
    public MoneyViewModel _totalCost = null!;

    [ObservableProperty]
    public OrderDeliveryDetailsViewModel _orderDelivery = null!;

    [ObservableProperty]
    public BuyerViewModel _buyer = null!;

    [ObservableProperty]
    public IReadOnlyCollection<OrderLineViewModel> _lines = [];

    public bool CanShip => Status == OrderStatus.Paid;
    public bool CanDeliver => Status == OrderStatus.Shipped;

    public string StatusText
        => OrderMappings.MapOrderStatusText(Status);

    public string StatusIcon
        => OrderMappings.MapOrderStatusIcon(Status);
    public bool CanGeneratePdf
        => Status == OrderStatus.Returned
        || Status == OrderStatus.Delivered;

    #endregion

    private readonly IOrderService _orderService;
    private readonly IImageService _imageService;
    private readonly IDocumentService _documentService;
    public OrderDetailsPageViewModel(AlertViewModel alert,
        IOrderService orderService,
        IImageService imageService,
        IDocumentService documentService) : base(alert)
    {
        _orderService = orderService;
        _imageService = imageService;
        _documentService = documentService;
    }

    public async Task InitializeAsync(Guid orderId)
    {
        var detailsResult = await _orderService.GetOrderDetails(orderId);
        var (ok, value) = HandleResult(detailsResult);
        if (!ok || value is null)
            return;

        BuildViewModel(value);
        await FetchImagePreviewAsync();
    }

    public void BuildViewModel(OrderDetailsResponse dto)
    {
        OrderId = dto.Id;
        SellerDisplayName = dto.SellerDisplayName;
        LinesCount = dto.LinesCount;
        TotalItemsCost = new MoneyViewModel(dto.TotalItemsCost);
        TotalCost = new MoneyViewModel(dto.TotalCost);
        OrderDelivery = OrderDeliveryDetailsViewModel.From(dto.DeliveryDetails);
        Buyer = new BuyerViewModel(dto.BuyerSnapshot);
        PurchasedDate = dto.PurchasedDate.ToLocalTime().ToString("dd/MM/yyyy, HH:mm");
        Status = dto.Status;
        Lines = dto.Lines
            .Select(OrderLineViewModel.From)
            .ToList();
    }

    [RelayCommand]
    public async Task DownloadPdf()
    {
        var bytes = await _documentService.DownloadOrderDetailsRaport(OrderId);
        var filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            $"order-details-{OrderId}.pdf");

        await File.WriteAllBytesAsync(filePath, bytes);

        Process.Start(new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true,
        });
    }


    [RelayCommand]
    public async Task ShipOrder()
    {
        if (!CanShip)
            return;

        var result = await _orderService.ShipOrder(OrderId);
        var (ok, value) = HandleResult(result, "Successfully shipped order.");
        if (!ok || value is null)
            return;

        Status = OrderStatus.Shipped;
    }

    [RelayCommand]
    public async Task DeliverOrder()
    {
        if (!CanDeliver)
            return;

        var result = await _orderService.DeliverOrder(OrderId);
        var (ok, value) = HandleResult(result, "Successfully shipped order.");
        if (!ok || value is null) return;

        Status = OrderStatus.Delivered;
    }

    // Fetching images from static resource -> saving to mem -> bitmap -> assign to line
    public async Task FetchImagePreviewAsync()
    {
        if (Lines is null || Lines.Count < 1)
            return;

        var tasks = Lines
            .Where(line => !string.IsNullOrWhiteSpace(line.Thumbnail.ImagePath))
            .Select(async line =>
            {
                var bytes = await _imageService.GetImageBytesAsync(line.Thumbnail.ImagePath);
                if (bytes is null)
                    return;

                using var ms = new MemoryStream(bytes);
                var bitmap = new Bitmap(ms);

                line.Thumbnail.Preview = bitmap;
            });

        await Task.WhenAll(tasks);
    }

    partial void OnStatusChanged(OrderStatus value)
    {
        OnPropertyChanged(nameof(StatusText));
        OnPropertyChanged(nameof(StatusIcon));
        OnPropertyChanged(nameof(CanShip));
        OnPropertyChanged(nameof(CanDeliver));
        OnPropertyChanged(nameof(CanGeneratePdf));
    }
}
