using Avalonia.Controls;
 using ByteBuy.UI.ViewModels;
 
 namespace ByteBuy.UI.Views;
 
 public partial class RolesPageView : UserControl
 {
     public RolesPageView()
     {
         InitializeComponent();
     }
     public RolesPageView(RolesPageViewModel vm)
     {
         InitializeComponent();
         DataContext = vm;
     }
 }