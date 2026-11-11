using Avalonia;
using Avalonia.Controls;

namespace ByteBuy.UI.Views.Shared
{
    public partial class Loader : UserControl
    {
        public static StyledProperty<string> MessageProperty =
            AvaloniaProperty.Register<Loader, string>(nameof(Message), "Loading...");

        public string Message
        {
            get => GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public Loader()
        {
            InitializeComponent();
        }
    }
}