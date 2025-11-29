using System;
using ByteBuy.Services.ModelsUI.Abstractions;

namespace ByteBuy.UI.ModelsUI.Employee;

public class RoleListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
