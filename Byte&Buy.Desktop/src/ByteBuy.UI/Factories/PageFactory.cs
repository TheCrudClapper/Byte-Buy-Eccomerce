using System;
using ByteBuy.UI.Data;
using ByteBuy.UI.ViewModels;
using PageViewModel = ByteBuy.UI.ViewModels.Base.PageViewModel;

namespace ByteBuy.UI.Factories;

public class PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
{
    public PageViewModel GetPageViewModel(ApplicationPageNames pageName)
        => factory.Invoke(pageName);
}