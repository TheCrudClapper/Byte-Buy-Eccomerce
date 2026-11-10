using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Charts;
using ByteBuy.UI.ViewModels.Dashboard;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Defaults;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DashboardPageViewModel : PageViewModel
{
    #region MVVM Fields
    [ObservableProperty]
    private ObservableCollection<OrderDashboardViewModel> _orders = [];

    [ObservableProperty]
    private ObservableCollection<KpiViewModel> _kpis = [];

    [ObservableProperty]
    private ObservableCollection<PieChartViewModel> _gmvBySellerData = [];

    [ObservableProperty]
    private DateTimePoint[] columnValues = [];

    [ObservableProperty]
    private double[] _gmvValues = [];

    [ObservableProperty]
    private string? _userFirstName;

    [ObservableProperty]
    private string? _userLastName;

    #endregion

    private readonly IOrderService _orderService;
    private readonly IStatisticsService _statisticsService;
    private readonly INotificationService _notificationService;
    public DashboardPageViewModel(IOrderService orderService,
        IStatisticsService statisticsService, ISessionService sessionService,
        INotificationService notificationService)
    {
        PageName = ApplicationPageNames.Dashboard;
        _orderService = orderService;
        _statisticsService = statisticsService;
        _notificationService = notificationService;
        UserFirstName = sessionService.UserFirstName;
        UserLastName = sessionService.UserLastName;
    }

    public async Task LoadDataAsync()
    {
        var t1 = LoadOrdersAsync();
        var t2 = LoadKpisAsync();
        var t3 = LoadGmvBySellerTypeAsync();
        var t4 = LoadOrdersAndGmvChartAsync();

        await Task.WhenAll(t1, t2, t3, t4);
    }

    private async Task LoadKpisAsync()
    {
        var kpisResult = await _statisticsService.GetKpisAsync();
        if (!kpisResult.Success)
        {
            _notificationService.ShowError(kpisResult.Error?.Description ?? "An error occurred while loading dashboard KPIs.");
            return;
        }

        Kpis = new ObservableCollection<KpiViewModel>(kpisResult.Value.Select(k => new KpiViewModel(k)));
    }

    private async Task LoadGmvBySellerTypeAsync()
    {
        var gmvResult = await _statisticsService.GetGmvBySellerTypeAsync();
        if (!gmvResult.Success)
        {
            _notificationService.ShowError(gmvResult.Error?.Description ?? "An error occurred while loading GMV data.");
            return;
        }

        GmvBySellerData = new ObservableCollection<PieChartViewModel>(gmvResult.Value.Select(g => new PieChartViewModel(g.Display, [g.GMVAmount.Amount])));
    }

    private async Task LoadOrdersAsync()
    {
        var ordersResult = await _orderService.GetDashboardOrdersAsync();
        if (!ordersResult.Success)
        {
            _notificationService.ShowError(ordersResult.Error?.Description ?? "An error occurred while loading dashboard orders.");
            return;
        }

        Orders = new ObservableCollection<OrderDashboardViewModel>(ordersResult.Value.Select(o => new OrderDashboardViewModel(o)));
    }

    private async Task LoadOrdersAndGmvChartAsync()
    {
        var result = await _statisticsService.GetOrdersGmvByMonthsAsync();
        if (!result.Success)
        {
            _notificationService.ShowError(result.Error?.Description ?? "An error occurred while loading the orders and GMV chart.");
            return;
        }

        ColumnValues = result.Value.Select(v => new DateTimePoint
        {
            DateTime = new DateTime(v.Year, v.Month, 1),
            Value = v.OrdersCount
        }).ToArray();

        GmvValues = result.Value.Select(v => (double)v.Gmv).ToArray();
    }


    public Func<DateTime, string> Formatter { get; set; } =
        date => date.ToString("MMMM dd");
}