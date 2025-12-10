using ByteBuy.UI.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DialogHostAvalonia;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Dialogs;

public partial class ConfirmationDialogViewModel : DialogViewModel
{

    [ObservableProperty]
    private string _message = "Do you want to delete selected item ?";

    [ObservableProperty]
    private string _icon = "avares://ByteBuy.UI/Assets/Images/regular/circle-info-solid-full.svg";

    [ObservableProperty]
    private string _cancelText = "Cancel";

    [ObservableProperty]
    private string _confirmText = "Confirm";

    public ConfirmationDialogViewModel() : base("Confirm") { }


    [RelayCommand]
    public async Task Confirm()
        => DialogHost.Close("MainDialogHost", true);

    [RelayCommand]
    public async Task Cancel()
        => DialogHost.Close("MainDialogHost", false);

}
