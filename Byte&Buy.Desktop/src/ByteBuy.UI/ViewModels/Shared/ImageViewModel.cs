using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.UI.ViewModels.Shared;

public partial class ImageViewModel : ObservableValidator
{
    public Guid? Id { get; }
    public string? Path { get; }
    public string FileName { get; } = string.Empty;
    public Bitmap? Preview { get; }

    public bool IsNew => Id is null;

    [ObservableProperty]
    private bool _isDeleted;

    [ObservableProperty]
    [Required, MaxLength(50)]
    private string _altText = string.Empty;

    //public Stream FileStream { get; } = fileStream;
    public byte[] FileBytes { get; } = [];

    //For existing picture
    public ImageViewModel(Guid id, string path, string altText, Bitmap preview)
    {
        Id = id;
        Path = path;
        AltText = altText;
        Preview = preview;
    }

    //For pictures added from file system
    public ImageViewModel(Bitmap preview, string fileName, byte[] bytes)
    {
        Preview = preview;
        FileName = fileName;
        FileBytes = bytes;
    }

    public void Validate()
    {
        ValidateAllProperties();
    }

    partial void OnAltTextChanged(string? oldValue, string newValue)
    {
        ValidateProperty(newValue, nameof(AltText));
    }
}