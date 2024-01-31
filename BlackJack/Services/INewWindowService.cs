
namespace BlackJack.Services;

/// <summary>
/// A service that opens new windows
/// </summary>
public interface INewWindowService
{
    Dictionary<string, object> ShowWindow(object viewModel);
    void CloseWindow();
}
