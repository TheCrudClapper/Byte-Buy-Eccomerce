using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;

namespace ByteBuy.UI.ViewModels.Base;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;

    private readonly CancellationTokenSource _pageCts = new();
    protected CancellationToken Token => _pageCts.Token;

    public virtual void OnNavigatedFrom()
    {
        _pageCts?.Cancel();
    }

    protected PageViewModel()
    {
    }
}