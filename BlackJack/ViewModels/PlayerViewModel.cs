using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BlackJack.ViewModels;

/// <summary>
/// ViewModel for the Player View, a help class to avoid dupulication in the MainViewModel
/// </summary>
public partial class PlayerViewModel : ObservableObject
{

    #region Properties

    /// <summary>
    /// Observable property that represents the player's information
    /// </summary>
    [ObservableProperty]
    private string info;

    /// <summary>
    /// Observable property that represents the player's visibility
    /// </summary>
    [ObservableProperty]
    private Visibility visibility;

    /// <summary>
    /// Observable property that represents the horizontal alignment of the player's cards
    /// </summary>
    [ObservableProperty]
    private HorizontalAlignment horizontalAlignment;

    /// <summary>
    /// Observable property that represents the player's buttons
    /// </summary>
    [ObservableProperty]
    private bool isButtonsEnabled = true;

    /// <summary>
    /// Observable property that represents a collection of player cards
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<BitmapImage> cards = new();

    #endregion Properties
}
