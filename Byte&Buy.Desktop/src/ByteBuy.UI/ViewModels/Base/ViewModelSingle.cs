using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelSingle
{
    #region  MVVM Fields

    #endregion
    [RelayCommand]
    protected abstract Task Save();
}