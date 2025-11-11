using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Employee;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Employee : ApplicationUser
{
    public AddressValueObj HomeAddress { get; set; } = null!;
    public Guid CompanyInfoId { get; set; }
    public CompanyInfo CompanyInfo { get; set; } = null!;

    private Employee(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static Result<Employee> CreateEmployee(EmployeeAddRequest request)
    {
        var result = AddressValueObj
            .Create(request.Street, request.HouseNumber, request.PostalCode, request.City, request.Country, request.FlatNumber);

        if (result.IsFailure)
            return Result.Failure<Employee>(Error.DummyError);

        var employee = new Employee(request.FirstName, request.LastName, request.Email);

        employee.HomeAddress = result.Value;

        return employee;
    }
}

