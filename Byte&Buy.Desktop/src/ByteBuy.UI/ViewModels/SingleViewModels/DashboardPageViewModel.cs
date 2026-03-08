using ByteBuy.Services.ServiceContracts;
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

    #endregion

    private readonly IOrderService _orderService;
    private readonly IStatisticsService _statisticsService;
    public DashboardPageViewModel(AlertViewModel alert, IOrderService orderService,
        IStatisticsService statisticsService) : base(alert)
    {
        _orderService = orderService;
        _statisticsService = statisticsService;
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
        var (ok, value) = HandleResult(kpisResult);
        if (!ok || value is null)
            return;

        Kpis = new ObservableCollection<KpiViewModel>(value.Select(k => new KpiViewModel(k)));
    }

    private async Task LoadGmvBySellerTypeAsync()
    {
        var gmvResult = await _statisticsService.GetGmvBySellerTypeAsync();
        var (ok, value) = HandleResult(gmvResult);
        if (!ok || value is null)
            return;

        GmvBySellerData = new ObservableCollection<PieChartViewModel>(value.Select(g => new PieChartViewModel(g.Display, [g.GMVAmount.Amount])));
    }

    private async Task LoadOrdersAsync()
    {
        var ordersResult = await _orderService.GetDashboardOrdersAsync();
        var (ok, value) = HandleResult(ordersResult);
        if (!ok || value is null)
            return;

        Orders = new ObservableCollection<OrderDashboardViewModel>(value.Select(o => new OrderDashboardViewModel(o)));
    }

    private async Task LoadOrdersAndGmvChartAsync()
    {
        var result = await _statisticsService.GetOrdersGmvByMonthsAsync();
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ColumnValues = value.Select(v => new DateTimePoint
        {
            DateTime = new DateTime(v.Year, v.Month, 1),
            Value = v.OrdersCount
        }).ToArray();

        GmvValues = value.Select(v => (double)v.Gmv).ToArray();
    }


    public Func<DateTime, string> Formatter { get; set; } =
        date => date.ToString("MMMM dd");
}