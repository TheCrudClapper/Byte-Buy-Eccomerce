using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class UsersPageView : UserControl
{
    public UsersPageView()
    {
        InitializeComponent();
    }

    public UsersPageView(UsersPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}