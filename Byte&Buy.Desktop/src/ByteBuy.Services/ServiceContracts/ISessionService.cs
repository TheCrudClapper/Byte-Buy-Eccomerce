namespace ByteBuy.Services.ServiceContracts;

public interface ISessionService
{
    string? UserFirstName { get; set; }
    string? UserLastName { get; set; }
    void ClearSessionData();
}
