using System;
using System.Collections.ObjectModel;
using ByteBuy.Services.DTO.Employee;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel : ViewModelBase
{
    public ObservableCollection<EmployeeResponse> Employees { get; set; } = new();

    public EmployeesPageViewModel()
    {
        Employees.Add(
            new EmployeeResponse(
                new Guid(),
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "725766544"
                )
            );
    }
}