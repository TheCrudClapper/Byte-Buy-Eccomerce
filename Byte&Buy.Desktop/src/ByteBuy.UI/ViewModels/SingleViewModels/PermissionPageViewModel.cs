using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.SingleViewModels;

public partial class PermissionPageViewModel : ViewModelSingle
{
    #region MVVM Properties

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    #endregion

    public PermissionPageViewModel(AlertViewModel alert) 
        : base(alert)
    {
       
    }

    protected override Task AddAsync()
    {
        throw new System.NotImplementedException();
    }

    protected override void Clear()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    protected override Task UpdateAsync()
    {
        throw new System.NotImplementedException();
    }
}
