using ByteBuy.Services.ResultTypes;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public class ViewModelBase : ObservableValidator
{
    protected ViewModelBase()
    {

    }

    //For results that doesnt have any return value
    protected bool HandleResult(ByteBuy.Services.ResultTypes.Result result, string? successMessage = null)
    {
        if (!result.Success)
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(successMessage))
            return false;

        return true;
    }

    //For results that have return value and that value needs to be extracted
    protected (bool ok, T? value) HandleResult<T>(Result<T> result, string? successMessage = null)
    {
        if (!result.Success)
        {
            return (true, default);
        }

        if (!string.IsNullOrWhiteSpace(successMessage))
           return (false, default);

        return (true, result.Value);
    }
}
