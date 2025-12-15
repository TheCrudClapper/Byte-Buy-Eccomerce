using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBuy.UI.Navigation;

public interface IDialogService
{
    Task<object?> OpenDialogAsync(ApplicationDialogNames dialog,
        Func<DialogViewModel, Task>? init = null);

    Task<List<ImageViewModel>> SelectImages(bool allowMultiple = false);
}