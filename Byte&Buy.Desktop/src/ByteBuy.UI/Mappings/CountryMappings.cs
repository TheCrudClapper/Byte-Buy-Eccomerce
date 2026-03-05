
using ByteBuy.Core.DTO.Country;
using ByteBuy.UI.ViewModels.Country;

namespace ByteBuy.UI.Mappings;

public static class CountryMappings
{
    public static CountryListItemViewModel ToListItem(this CountryResponse response, int index)
    {
        return new CountryListItemViewModel
        {
            RowNumber = index,
            Code = response.Code,
            Id = response.Id,
            Name = response.Name,
        };
    }
}
