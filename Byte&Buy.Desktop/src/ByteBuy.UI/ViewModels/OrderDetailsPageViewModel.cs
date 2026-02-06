using Avalonia.Media.Imaging;
using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Order.Buyer;
using ByteBuy.UI.ViewModels.Order.OrderDelivery;
using ByteBuy.UI.ViewModels.Order.OrderLine;
using ByteBuy.UI.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace ByteBuy.UI.ViewModels;

public partial class OrderDetailsPageViewModel : PageViewModel
{
    #region MVVM Fields
    public string StatusText { get; set; } = null!;
    public string StatusIcon { get; set; } = null!;

    public string PurchasedDate { get; set; } = null!;
    public string SellerDisplayName { get; set; }  = null!;
    public int LinesCount { get; set; }

    public MoneyViewModel TotalItemsCost { get; set; } = null!;
    public MoneyViewModel TotalCost { get; set; } = null!;

    public OrderDeliveryDetailsViewModel OrderDelivery { get; set; } = null!;
    public BuyerViewModel Buyer { get; set; } = null!;
    public IReadOnlyCollection<OrderLineViewModel> Lines { get; set; } = [];

    #endregion

    private readonly IOrderService _orderService;
    private readonly IImageService _imageService;
    public OrderDetailsPageViewModel(AlertViewModel alert,
        IOrderService orderService,
        IImageService imageService) : base(alert)
    {
        _orderService = orderService;
        _imageService = imageService;
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

    public async void BuildViewModel(OrderDetailsResponse dto)
    {
        StatusText = dto.Status.ToString();
        StatusIcon = OrderMappings.MapOrderStatusIcon(dto.Status);
        SellerDisplayName = dto.SellerDisplayName;
        LinesCount = dto.LinesCount;
        TotalItemsCost = new MoneyViewModel(dto.TotalItemsCost);
        TotalCost = new MoneyViewModel(dto.TotalCost);
        OrderDelivery = OrderDeliveryDetailsViewModel.From(dto.DeliveryDetails);
        Buyer = new BuyerViewModel(dto.BuyerSnapshot);
        PurchasedDate = dto.PurchasedDate.ToString("dd/MM/yyyy, HH:mm");

        Lines = dto.Lines
            .Select(OrderLineViewModel.From)
            .ToList();
    }

    // Fetching images from static resource -> saving to mem -> bitmap -> assign to line
    public async Task FetchImagePreviewAsync()
    {
        if (Lines is null || !Lines.Any())
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
}
