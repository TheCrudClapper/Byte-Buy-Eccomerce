using ByteBuy.Services.Models;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SessionService : ISessionService
{
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }

    public void ClearSessionData()
    {
        UserFirstName = null;
        UserFirstName = null;
    }
}
