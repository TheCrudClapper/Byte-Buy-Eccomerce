using System;
using System.Collections.ObjectModel;
using ByteBuy.Services.DTO.Employee;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;

namespace ByteBuy.UI.ViewModels;

public partial class EmployeesPageViewModel : PageViewModel
{
    public ObservableCollection<EmployeeResponse> Employees { get; set; } = [];

    public EmployeesPageViewModel()
    {
        PageName = ApplicationPageNames.Employees;
        Employees.Add(
            new EmployeeResponse(
                Guid.NewGuid(),
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