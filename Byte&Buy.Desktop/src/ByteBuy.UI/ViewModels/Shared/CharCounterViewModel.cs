using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class CharCounterViewModel : ObservableObject
{
    [ObservableProperty] private int _currentCount;
    [ObservableProperty] private int _maxCount;
    [ObservableProperty] private bool _exceeded;
    public CharCounterViewModel(int maxCount)
    {
        CurrentCount = 0;
        MaxCount = maxCount;
    }
    partial void OnCurrentCountChanged(int value)
    {
        Exceeded = value > MaxCount;
    }
}
