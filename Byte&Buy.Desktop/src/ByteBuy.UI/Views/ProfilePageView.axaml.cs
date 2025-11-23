using Avalonia.Controls;
using ByteBuy.UI.ViewModels;

namespace ByteBuy.UI.Views;

public partial class ProfilePageView : UserControl
{
    public ProfilePageView()
    {
        InitializeComponent();
    }

    public ProfilePageView(ProfilePageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}