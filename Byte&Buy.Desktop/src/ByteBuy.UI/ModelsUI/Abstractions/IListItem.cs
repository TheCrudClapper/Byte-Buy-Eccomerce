using System;

namespace ByteBuy.UI.ModelsUI.Abstractions;

public interface IListItem
{
    int RowNumber { get; set; }
    Guid Id { get; init; }
}
