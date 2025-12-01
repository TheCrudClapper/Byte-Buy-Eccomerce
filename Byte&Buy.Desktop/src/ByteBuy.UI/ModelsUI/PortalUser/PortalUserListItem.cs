using ByteBuy.UI.ModelsUI.Abstractions;
using System;

namespace ByteBuy.UI.ModelsUI.PortalUser;

public class PortalUserListItem : IListItem
{
    public int RowNumber { get; set; }
    public Guid Id { get; init; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;

    public PortalUserListItem()
    {

    }
}