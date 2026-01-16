namespace ByteBuy.Core.ResultTypes;
public static class EmployeeErrors
{
    public static readonly Error CompanyNotFound = new(
        ErrorType.NotFound, "Employee.Company", "You can't assign employee to unknown company");
}

