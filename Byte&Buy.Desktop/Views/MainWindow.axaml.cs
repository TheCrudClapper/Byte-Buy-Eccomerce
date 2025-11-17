using Avalonia.Controls;
using ByteBuy.Desktop.ViewModels;

namespace ByteBuy.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}