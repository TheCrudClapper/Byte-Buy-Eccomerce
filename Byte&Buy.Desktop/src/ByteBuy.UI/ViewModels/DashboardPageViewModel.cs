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
    private ObservableCollection<PieChartViewModel> gmvBySellerData = [];
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
        await LoadOrdersAsync();
        await LoadKpisAsync();
        await LoadGmvBySellerType();
    }

    private async Task LoadKpisAsync()
    {
        var kpisResult = await _statisticsService.GetKpis();
        var (ok, value) = HandleResult(kpisResult);
        if (!ok || value is null)
            return;

        Kpis = new ObservableCollection<KpiViewModel>(value.Select(k => new KpiViewModel(k)));
    }

    private async Task LoadGmvBySellerType()
    {
        var gmvResult = await _statisticsService.GetGmvBySellerType();
        var (ok, value) = HandleResult(gmvResult);
        if (!ok || value is null)
            return;

        GmvBySellerData = new ObservableCollection<PieChartViewModel>(value.Select(g => new PieChartViewModel(g.Display, [g.GMVAmount.Amount])));
    }

    private async Task LoadOrdersAsync()
    {
        var ordersResult = await _orderService.GetDashboardOrders();
        var (ok, value) = HandleResult(ordersResult);
        if (!ok || value is null)
            return;

        Orders = new ObservableCollection<OrderDashboardViewModel>(value.Select(o => new OrderDashboardViewModel(o)));
    }


    public double[] Values { get; set; } = { 12, 14, 16, 17, 1 };
    public double[] Values2 { get; set; } = { 20, 63, 123, 346, 500 };

    public DateTimePoint[] ColumnValues { get; set; } = [
        new() { DateTime = new(2026, 1, 1), Value = 3 },
        new() { DateTime = new(2026, 1, 2), Value = 6 },
        new() { DateTime = new(2026, 1, 3), Value = 5 },
        new() { DateTime = new(2026, 1, 4), Value = 3 },
        new() { DateTime = new(2026, 1, 5), Value = 5 },
        new() { DateTime = new(2026, 1, 6), Value = 8 },
        new() { DateTime = new(2026, 1, 7), Value = 6 }
    ];

    public Func<DateTime, string> Formatter { get; set; } =
        date => date.ToString("MMMM dd");
}