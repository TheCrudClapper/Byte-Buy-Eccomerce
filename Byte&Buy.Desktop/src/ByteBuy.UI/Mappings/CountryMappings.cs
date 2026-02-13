
using ByteBuy.Core.DTO.Country;
using ByteBuy.UI.ModelsUI.Country;

namespace ByteBuy.UI.Mappings;

public static class CountryMappings
{
    public static CountryListItem ToListItem(this CountryResponse response, int index)
    {
        return new CountryListItem
        {
            RowNumber = index,
            Code = response.Code,
            Id = response.Id,
            Name = response.Name,
        };
    }
}
