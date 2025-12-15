using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.Mappings;
using ByteBuy.UI.Navigation;
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
    private int _descriptionCharCount;

    [ObservableProperty]
    [Required, MaxLength(2000)]
    private string _description = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _categories = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse> _conditions = [];

    [ObservableProperty]
    private ObservableCollection<ImageViewModel> _images = [];

    public bool ImageContainerVisible => Images.Count == 0;

    #endregion
    private readonly IItemService _itemService;
    private readonly IConditionService _conditionService;
    private readonly ICategoryService _categoryService;
    private readonly IDialogService _dialogService;
    public ItemPageViewModel(AlertViewModel alert,
        IItemService itemService,
        IConditionService conditionService,
        ICategoryService categoryService,
        IDialogService dialogService) : base(alert)
    {
        _conditionService = conditionService;
        _categoryService = categoryService;
        _itemService = itemService;
        _dialogService = dialogService;
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

    public async Task InitializeForEdit(Guid id)
    {
        EditingItemId = id;
        IsEditMode = true;
    }
    protected override async Task AddItem()
    {
        var request = ItemMappings.MapToRequest(this);
        var result = await _itemService.Add(request);
        HandleResult(result, "Successfully added new item !");
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


    protected override async Task Save()
    {
        ValidateAllProperties();
        bool anyImageErrors = false;
        foreach (var img in Images)
        {
            img.Validate();
            if (img.HasErrors)
                anyImageErrors = true;
        }

        if (HasErrors || anyImageErrors)
            return;

        await (IsEditMode switch
        {
            true => UpdateItem(),
            false => AddItem(),
        });
    }

    partial void OnImagesChanged(ObservableCollection<ImageViewModel>? oldValue, ObservableCollection<ImageViewModel> newValue)
    {
        if (oldValue != null)
            oldValue.CollectionChanged -= OnImagesCollectionChanged;
        if (newValue != null)
            newValue.CollectionChanged += OnImagesCollectionChanged;

        OnPropertyChanged(nameof(ImageContainerVisible));
    }

    private void OnImagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ImageContainerVisible));
    }

    [RelayCommand]
    public async Task AddImages()
    {
        var result = await _dialogService.SelectImages(true);
        foreach (var image in result)
        {
            Images.Add(image);
        }
    }

    [RelayCommand]
    public void DeleteImage(ImageViewModel item)
    {
        Images.Remove(item);
    }

    partial void OnDescriptionChanged(string value)
    {
        DescriptionCharCount = value.Length;
        ValidateProperty(value, nameof(Description));
    }
}
