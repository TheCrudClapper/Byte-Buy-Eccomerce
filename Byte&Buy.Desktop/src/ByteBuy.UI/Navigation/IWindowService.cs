using ByteBuy.UI.Data;

namespace ByteBuy.UI.Navigation;

public interface IWindowService
{
    void ShowWindow(ApplicationWindowNames name, bool hideOldOne = false);
    void ShutdownApplication();
}
