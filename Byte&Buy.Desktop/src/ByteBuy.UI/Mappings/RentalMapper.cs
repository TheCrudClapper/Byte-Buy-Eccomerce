using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental.Enum;
using ByteBuy.UI.ModelsUI.Rental;

namespace ByteBuy.UI.Mappings;

public static class RentalMapper
{
    public static RentalListItem ToListItem(this CompanyRentalLenderResponse dto, int index)
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
            RowNumber = index + 1,
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
}
