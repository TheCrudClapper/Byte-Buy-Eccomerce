using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.ViewModels.Base;
using ByteBuy.UI.ViewModels.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ItemPageViewModel : ViewModelSingle
{
    #region MVVM Fields
    [ObservableProperty]
    [Required, MaxLength(75)]
    private string _name = string.Empty;

    [ObservableProperty]
    [Range(1, double.MaxValue)]
    private int _stockQuantity;

    [ObservableProperty]
    [Required(ErrorMessage = "You need to select category")]
    private SelectListItemResponse? _selectedCategory;

    [ObservableProperty]
    [Required(ErrorMessage = "You need to select condition")]
    private SelectListItemResponse? _selectedCondition;

    [ObservableProperty]
    [Required, MaxLength(2000)]
    private string _description = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _categories = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _conditions = [];

    [ObservableProperty]
    private ObservableCollection<ItemImageViewModel> _images = [];

    public bool ImageContainerVisible => Images.Count == 0;

    #endregion
    private readonly IItemService _itemService;
    private readonly IConditionService _conditionService;
    private readonly ICategoryService _categoryService;
    public ItemPageViewModel(AlertViewModel alert,
        IItemService itemService,
        IConditionService conditionService,
        ICategoryService categoryService) : base(alert)
    {
        _conditionService = conditionService;
        _categoryService = categoryService;
        _itemService = itemService;
        Images.CollectionChanged += OnImagesCollectionChanged;
        _ = Initialize();
    }

    private async Task Initialize()
    {
        var roleResult = await _conditionService.GetSelectList();
        Conditions = new ObservableCollection<SelectListItemResponse>(roleResult?.Value ?? []);

        var categoryResult = await _categoryService.GetSelectList();
        Categories = new ObservableCollection<SelectListItemResponse>(categoryResult?.Value ?? []);
    }

    protected override Task AddItem()
    {
        throw new System.NotImplementedException();
    }

    protected override void Clear()
    {
        Name = string.Empty;
        Description = string.Empty;
        SelectedCondition = null;
        SelectedCategory = null;
        StockQuantity = 0;
    }

    protected override Task UpdateItem()
    {
        throw new System.NotImplementedException();
    }


    partial void OnImagesChanged(ObservableCollection<ItemImageViewModel>? oldValue, ObservableCollection<ItemImageViewModel> newValue)
    {
        if(oldValue != null)
            oldValue.CollectionChanged -= OnImagesCollectionChanged;
        if(newValue != null)
            newValue.CollectionChanged += OnImagesCollectionChanged;

        OnPropertyChanged(nameof(ImageContainerVisible));
    }

    private void OnImagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ImageContainerVisible));
    }

    [RelayCommand]
    public void DeleteImage(ItemImageViewModel item) 
    {
        Images.Remove(item);
    }
}
