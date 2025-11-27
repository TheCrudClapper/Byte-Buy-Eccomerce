using ByteBuy.Services.ModelsUI.Abstractions;

namespace ByteBuy.Services.ModelsUI.Employee;

public class RoleListItem : IDataGridItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
