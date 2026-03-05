using System;

namespace ByteBuy.UI.ViewModels.Base;

public interface IListItemViewModel
{
    int RowNumber { get; set; }
    Guid Id { get; init; }
}
