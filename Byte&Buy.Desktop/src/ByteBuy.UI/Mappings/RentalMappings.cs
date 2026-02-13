using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental.Enum;
using ByteBuy.UI.ModelsUI.Rental;
using System;

namespace ByteBuy.UI.Mappings;

public static class RentalMappings
{
    public static RentalListItem ToListItem(this CompanyRentalLenderListResponse dto, int index)
    {
        return new RentalListItem()
        {
            BorrowerEmail = dto.BorrowerEmail,
            EndingRentalDate = dto.EndingRentalDate,
            Id = dto.Id,
            ItemName = dto.ItemName,
            Quantity = dto.Quantity,
            RentalDays = dto.RentalDays,
            StartingRentalDate = dto.StartingRentalDate,
            RowNumber = index,
            Status = dto.Status switch
            {
                RentalStatus.Overdue => "Overdue",
                RentalStatus.Created => "Created",
                RentalStatus.Active => "Active",
                RentalStatus.Completed => "Completed",
                _ => "Unknowm"
            }
        };
    }

    public static string MapRentalStatusIcon(RentalStatus status)
    {
        return status switch
        {
            RentalStatus.Created => "avares://ByteBuy.UI/Assets/Images/regular/square-plus-solid-full.svg",
            RentalStatus.Active => "avares://ByteBuy.UI/Assets/Images/regular/calendar-days-regular-full.svg",
            RentalStatus.Overdue => "avares://ByteBuy.UI/Assets/Images/regular/calendar-xmark-regular-full.svg",
            RentalStatus.Completed => "avares://ByteBuy.UI/Assets/Images/regular/calendar-check-solid-full.svg",
            _ => throw new NotSupportedException()
        };
    }
}
