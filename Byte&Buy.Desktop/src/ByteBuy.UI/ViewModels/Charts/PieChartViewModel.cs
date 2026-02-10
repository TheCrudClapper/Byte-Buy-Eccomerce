namespace ByteBuy.UI.ViewModels.Charts;

public class PieChartViewModel
{
    public string Name { get; set; } = null!;
    public decimal[] Values { get; set; } = [];

    public PieChartViewModel(string name, decimal[] values)
    {
        Values = values;
        Name = name;
    }
}
