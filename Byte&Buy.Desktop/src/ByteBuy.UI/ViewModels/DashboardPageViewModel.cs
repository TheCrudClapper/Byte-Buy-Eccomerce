using ByteBuy.Services.DTO.Order;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Dashboard;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class DashboardPageViewModel : PageViewModel
{
    #region MVVM Fields
    [ObservableProperty]
    private ObservableCollection<OrderDashboardViewModel> _orders = [];
    #endregion
    
    private readonly IOrderService _orderService;

    public DashboardPageViewModel(AlertViewModel alert, IOrderService orderService) : base(alert)
    {
        _orderService = orderService;
    }

    public async Task LoadDataAsync()
    {
        var ordersResult = await _orderService.GetDashboardOrders();
        var (ok, value) = HandleResult(ordersResult);
        if (!ok || value is null)
            return;

        Orders = new ObservableCollection<OrderDashboardViewModel>(value.Select(o => new OrderDashboardViewModel(o)));
    }

    [ObservableProperty]
    private string _test = "Just a test";

    public double[] Values { get; set; } = { 12, 14, 16, 17, 1 };
    public double[] Values2 { get; set; } = { 20, 63, 123, 346, 500 };

    public PieData[] Data { get; set; } = [
        new("Company", 20000312),
        new("Private", 20000312)];

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
public class PieData(string name, double value)
{
    public string Name { get; set; } = name;
    public double[] Values { get; set; } = [value];
}