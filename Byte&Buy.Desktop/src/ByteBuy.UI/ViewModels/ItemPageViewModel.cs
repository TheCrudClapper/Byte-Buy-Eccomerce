using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public class ItemPageViewModel : ViewModelSingle
{
    public ItemPageViewModel(AlertViewModel alert) : base(alert)
    {
    }

    protected override Task AddItem()
    {
        throw new System.NotImplementedException();
    }

    protected override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override Task UpdateItem()
    {
        throw new System.NotImplementedException();
    }
}
