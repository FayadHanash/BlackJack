using System.Windows;
using BlackJack.ViewModels;
using BlackJack.Views;

namespace BlackJack.Services;

/// <summary>
/// A service that opens the NewWindow
/// </summary>
public class NewWindowService : INewWindowService
{
    /// <summary>
    /// The window that will be opened
    /// </summary>
    private Window _window;

    /// <summary>
    /// Method that shows the window and returns the result of the window as a dictionary
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public Dictionary<string, object> ShowWindow(object viewModel)
    {
        _window = new NewWindow();
        _window.DataContext = viewModel;
        _window.ShowDialog();
        return (_window.DataContext as NewViewModel).Data;
    }

    /// <summary>
    /// Method that closes the window
    /// </summary>
    public void CloseWindow()
    {
        _window.Close();
    }
}


