using System.ComponentModel;
using System.Reflection;

namespace ByteBuy.Core.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Extracts Enum Description
    /// </summary>
    /// <param name="value">Enum type to extract description from</param>
    /// <returns></returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field is null)
            return value.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}
