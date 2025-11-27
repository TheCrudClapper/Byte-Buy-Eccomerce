using ByteBuy.Services.ModelsUI.Abstractions;

namespace ByteBuy.Services.ModelsUI.Employee;

public class EmployeeListItem : IDataGridItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}

