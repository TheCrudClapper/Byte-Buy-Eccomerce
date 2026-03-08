using Avalonia.Media.Imaging;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.UI.DataAdnotations;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBuy.UI.ViewModels;

public partial class ItemPageViewModel : ViewModelSingle
{
    #region MVVM Fields
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MaxLength(75, ErrorMessage = "Name must have at most 75 characters")]
    [MinLength(16, ErrorMessage = "Name must have at least 16 characters")]
    private string _name = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [StockQuantityValidation]
    private int _stockQuantity;

    [ObservableProperty]
    private int _currentStockQuantity;

    [ObservableProperty]
    [Required(ErrorMessage = "You need to select category")]
    private SelectListItemResponse<Guid>? _selectedCategory;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "You need to select condition")]
    private SelectListItemResponse<Guid>? _selectedCondition;

    [ObservableProperty]
    private int _descriptionCharCount;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required, MaxLength(2000)]
    private string _description = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _categories = [];

    [ObservableProperty]
    private ObservableCollection<SelectListItemResponse<Guid>> _conditions = [];

    [ObservableProperty]
    private ObservableCollection<ImageViewModel> _images = [];

    public bool ImageContainerVisible => !Images.Any(i => !i.IsDeleted);

    #endregion

    private readonly IItemService _itemService;
    private readonly IConditionService _conditionService;
    private readonly ICategoryService _categoryService;
    private readonly IImageService _imageService;
    private readonly IDialogService _dialogService;
    public ItemPageViewModel(AlertViewModel alert,
        IItemService itemService,
        IConditionService conditionService,
        ICategoryService categoryService,
        IDialogService dialogService,
        IImageService imageService) : base(alert)
    {
        _conditionService = conditionService;
        _categoryService = categoryService;
        _itemService = itemService;
        _dialogService = dialogService;
        _imageService = imageService;
        Images.CollectionChanged += OnImagesCollectionChanged;
    }

    protected override async Task InitializeAsync()
    {
        var conditionTask = _conditionService.GetSelectListAsync();
        var categoryTask = _categoryService.GetSelectList();

        await Task.WhenAll(conditionTask, categoryTask);

        var cond = await conditionTask;
        var cat = await categoryTask;


        Conditions = new ObservableCollection<SelectListItemResponse<Guid>>(cond.Value ?? []);

        Categories = new ObservableCollection<SelectListItemResponse<Guid>>(cat.Value ?? []);
    }

    public async Task InitializeForEdit(Guid id)
    {
        EditingItemId = id;
        IsEditMode = true;
        await InitializeAsync();

        var result = await _itemService.GetById(id);
        var (ok, value) = HandleResult(result);
        if (!ok || value is null)
            return;

        ItemMappings.MapFromResponse(this, value);

        Images.Clear();

        var tasks = value.Images.Select(async img =>
        {
            var bytes = await _imageService.GetImageBytesAsync(img.ImagePath);
            if (bytes is null) return;

            using var ms = new MemoryStream(bytes);
            var bmp = new Bitmap(ms);

            Images.Add(new ImageViewModel(img.Id, img.ImagePath, img.AltText, bmp));
        });

        await Task.WhenAll(tasks);
    }
    protected override async Task AddItem()
    {
        var request = ItemMappings.MapAddToRequest(this);
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

    protected override async Task UpdateItem()
    {
        if (EditingItemId is null)
            return;

        var deletedCount = Images.Count(i => i.IsDeleted);
        var newCount = Images.Count(i => i.IsNew);

        if (Images.Count - deletedCount == 0 && newCount == 0)
        {
            Alert.ShowErrorAlert("You need to add at least one new image.");
            return;
        }

        var request = ItemMappings.MapToUpdateRequest(this);
        var result = await _itemService.Update(EditingItemId.Value, request);
        HandleResult(result, "Successfully updated item !");
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
        if (item.IsNew)
        {
            Images.Remove(item);
            return;
        }

        item.IsDeleted = true;
        OnPropertyChanged(nameof(item.IsDeleted));
        OnPropertyChanged(nameof(ImageContainerVisible));
    }

    partial void OnDescriptionChanged(string value)
    {
        DescriptionCharCount = value.Length;
        ValidateProperty(value, nameof(Description));
    }
}
