using ByteBuy.Services.ResultTypes;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ByteBuy.UI.ViewModels.Base;

public class ViewModelBase : ObservableValidator
{
    public AlertViewModel Alert { get; }

    protected ViewModelBase(AlertViewModel alert)
    {
        Alert = alert;
    }

    //For results that doesnt have any return value
    protected bool HandleResult(Result result, string? successMessage = null)
    {
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return false;
        }

        if (!string.IsNullOrWhiteSpace(successMessage))
            Alert.ShowSuccessAlert(successMessage);

        return true;
    }

    //For results that have return value and that value needs to be extracted
    protected (bool ok, T? value) HandleResult<T>(Result<T> result, string? successMessage = null)
    {
        if (!result.Success)
        {
            Alert.ShowErrorAlert(result.Error!.Description);
            return (false, default);
        }

        if (!string.IsNullOrWhiteSpace(successMessage))
            Alert.ShowSuccessAlert(successMessage);

        return (true, result.Value);
    }
}
