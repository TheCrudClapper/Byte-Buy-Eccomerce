namespace ByteBuy.Core.ResultTypes;

public static class EmployeeErrors
{
    public static readonly Error EmployeeCreationFailed = new(ErrorType.Unexpected,
        "Employee.Add", "Employee creation failed, try again later");

    public static readonly Error EmployeeUpdateFailed = new(ErrorType.Unexpected,
        "Employee.Update", "Employee updation failed, try again later");

    public static readonly Error CompanyNotFound = new(
        ErrorType.NotFound, "Employee.Company", "You can't assign employee to unknown company");
}

