namespace ByteBuy.UI.Helpers;

public static class PaginationHelper
{
    public static int CalculateItemIndex(int index, int pageSize, int pageNumber)
        => (pageNumber - 1) * pageSize + index + 1;
}
