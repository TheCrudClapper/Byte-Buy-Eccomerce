namespace ByteBuy.Infrastructure.Helpers;

public static class QueryStringHelper
{
    public static string ToQueryString(string baseUrl, object query)
    {
        var props = query.GetType()
            .GetProperties()
            .Where(p => p.GetValue(query) != null)
            .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(query)!.ToString()!)}");

        return $"{baseUrl}?{string.Join("&", props)}";
    }
}
