using ByteBuy.Services.Pagination;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Data;
using ByteBuy.UI.Helpers;
using ByteBuy.UI.Navigation;
using ByteBuy.UI.Services;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels.Base;

public abstract partial class ViewModelMany<TDataGridItem, ServiceType> : PageViewModel
    where TDataGridItem : IListItemViewModel
    where ServiceType : IBaseService
{
    #region MVVM Fields

    [ObservableProperty]
    private int _pageNumber = 1;

    [ObservableProperty]
    private int _pageSize = 10;

    [ObservableProperty]
    private int _totalCount;

    [ObservableProperty]
    private int _totalPages;

    [ObservableProperty]
    private int _itemsCount;

    [ObservableProperty]
    private bool _hasNextPage;

    [ObservableProperty]
    private bool _hasPreviousPage;

    [ObservableProperty]
    private TDataGridItem? _selectedItem;

    [ObservableProperty]
    private ObservableCollection<TDataGridItem> _items = [];
    #endregion

    protected INavigationService Navigation;
    protected IDialogService DialogNavigation;
    protected readonly ServiceType Service;
    protected readonly INotificationService NotificationService;
    protected readonly ILoadingService LoadingService;

    protected ViewModelMany(
        INavigationService navigation,
        IDialogService dialogNavigation,
        ServiceType service,
        INotificationService notificationService,
        ILoadingService loadingService)
    {
        Navigation = navigation;
        DialogNavigation = dialogNavigation;
        Service = service;
        NotificationService = notificationService;
        LoadingService = loadingService;
    }

    // Delete after all pages gets pagination
    partial void OnItemsChanged(ObservableCollection<TDataGridItem> value)
    {
        ItemsCount = value.Count;
    }

    [RelayCommand]
    protected virtual async Task Delete(TDataGridItem item)
    {
        var decision = await DialogNavigation
            .OpenDialogAsync(ApplicationDialogNames.Confirm);

        if (decision is bool ok && ok)
        {
            var result = await Service.DeleteByIdAsync(item.Id);
            if (!result.Success)
            {
                NotificationService.ShowError(result.Error?.Description ?? "An error occurred while deleting the item.");
                return;
            }

            Items.Remove(item);
            NotificationService.ShowSuccess("Successfully deleted item !");
        }
        return;
    }

    [RelayCommand]
    protected abstract Task EditAsync(TDataGridItem item);

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            await LoadingService.ShowAsync("Loading data...");
            await LoadDataCoreAsync();
        }
        catch (OperationCanceledException) when (Token.IsCancellationRequested)
        {
        }
        finally
        {
            await LoadingService.HideAsync();
        }
    }

    protected abstract Task LoadDataCoreAsync();

    [RelayCommand]
    public virtual Task ClearFiltersAsync()
    {
        throw new NotImplementedException();
    }

    protected void ApplyPagination<T>(PagedList<T> value, Func<T, int, TDataGridItem> map)
    {
        PageNumber = value.Metadata.PageNumber;

        Items = new ObservableCollection<TDataGridItem>(
            value.Items.Select((item, i) =>
                map(item, PaginationHelper.CalculateItemIndex(i, PageSize, PageNumber))));

        TotalCount = value.Metadata.TotalCount;
        HasNextPage = value.Metadata.HasNext;
        TotalPages = value.Metadata.TotalPages;
        HasPreviousPage = value.Metadata.HasPrevious;
    }

    [RelayCommand]
    protected abstract Task AddAsync();

    [RelayCommand]
    protected async Task NextPage()
    {
        if (!HasNextPage) return;
        PageNumber++;
        await LoadDataAsync();
    }

    [RelayCommand]
    protected async Task PreviousPage()
    {
        if (!HasPreviousPage) return;
        PageNumber--;
        await LoadDataAsync();
    }
}