using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Extensions;

namespace ByteBuy.Core.Helpers;

public class EnumToSelectListMapper
{
    public static IReadOnlyCollection<SelectListItemResponse<int>> EnumToSelectLists<TEnum>()
        where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .Select(e => new SelectListItemResponse<int>(
                Convert.ToInt32(e),
                e.GetDescription()))
                .ToArray();
    }
}
