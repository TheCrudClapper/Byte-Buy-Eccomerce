using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class AccountPageView : UserControl
{
    //Design Time
    public AccountPageView()
    {
        InitializeComponent();
    }

    //Runtime
    public AccountPageView(AccountPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}