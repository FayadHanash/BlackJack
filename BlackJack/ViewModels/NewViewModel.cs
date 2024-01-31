using BlackJack.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace BlackJack.ViewModels;

/// <summary>
/// ViewModel for the New Game View
/// </summary>
public partial class NewViewModel : ObservableObject
{
    #region Fields
    /// <summary>
    /// Field that represents the INewWindowService object
    /// </summary>
    private readonly INewWindowService _windowService;

    /// <summary>
    /// Field that represents the data object
    /// </summary>
    public Dictionary<string, object> Data { get; private set; } = null;

    /// <summary>
    /// Field that represents the enablement of player' buttons
    /// </summary>
    (bool, bool, bool) IsPlayersEnabled = new();

    #endregion Fields

    #region Properties

    /// <summary>
    /// Observable property that represents number of decks
    /// </summary>
    [ObservableProperty]
    private List<int> numberOfDecks;

    /// <summary>
    /// Observable property that represents the number of deck
    /// </summary>
    [ObservableProperty]
    private int numberOfDeck;

    /// <summary>
    /// Observable property that represents number of players
    /// </summary>
    [ObservableProperty]
    private List<int> numberOfPlayers;

    /// <summary>
    /// Observable property that represents the number of player
    /// </summary>
    [ObservableProperty]
    private int numberOfPlayer;

    /// <summary>
    /// Observable property that represents the dealer name
    /// </summary>
    [ObservableProperty]
    private string dealerName;

    /// <summary>
    /// Observable property that represents the player 1 name
    /// </summary>
    [ObservableProperty]
    private string player1Name;

    /// <summary>
    /// Observable property that represents the player 2 name
    /// </summary>
    [ObservableProperty]
    private string player2Name;

    /// <summary>
    /// Observable property that represents the player 3 name
    /// </summary>
    [ObservableProperty]
    private string player3Name;

    /// <summary>
    /// Observable property that represents whether the player 2 button is enabled or not.
    /// </summary>
    [ObservableProperty]
    private bool isPlayer2Enabled;

    /// <summary>
    /// Observable property that represents whether the player 3 button is enabled or not.
    /// </summary>
    [ObservableProperty]
    private bool isPlayer3Enabled;

    #endregion Properties

    #region Constructors
    /// <summary>
    /// Default constructor
    /// </summary>
    public NewViewModel()
    {
    }

    /// <summary>
    /// Constructor that takes a INewWindowService object
    /// </summary>
    /// <param name="windowService"></param>
    public NewViewModel(INewWindowService windowService)
    {
        _windowService = windowService;
        NumberOfPlayers = new List<int>() { 1, 2, 3 };
        NumberOfDecks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        NumberOfPlayer = 1;
        NumberOfDeck = 1;
    }

    #endregion Constructors

    #region Commands

    /// <summary>
    /// Mrthod that saves the changes in the Data dictionary
    /// </summary>
    [RelayCommand]
    private void Save()
    {
        if (GetInputs())
        {
            Data = new()
            {
                { "DealerName", DealerName },
                { "Player1Name", Player1Name },
                { "Player2Name", Player2Name },
                { "Player3Name", Player3Name },
                { "NumberOfPlayers", NumberOfPlayer },
                { "NumberOfDecks", NumberOfDeck }
            };
            _windowService.CloseWindow();
        }
        else
        {
            Data = null;
        }

    }

    /// <summary>
    /// Method that close the window
    /// </summary>
    [RelayCommand]
    private void Close()
    {
        Data = null;
        _windowService.CloseWindow();
    }

    /// <summary>
    /// An event handler method that adjusts the enablement of players 
    /// </summary>
    /// <param name="value"></param>
    partial void OnNumberOfPlayerChanging(int value)
    {

        if (value == 1)
        {
            IsPlayer2Enabled = false;
            IsPlayer3Enabled = false;
            IsPlayersEnabled = (true, false, false);
        }
        else if (value == 2)
        {
            IsPlayer2Enabled = true;
            IsPlayer3Enabled = false;
            IsPlayersEnabled = (true, true, false);
        }
        else if (value == 3)
        {
            IsPlayer2Enabled = true;
            IsPlayer3Enabled = true;
            IsPlayersEnabled = (true, true, true);
        }

    }

    /// <summary>
    /// Method that checks the user entries 
    /// </summary>
    /// <returns></returns>
    private bool GetInputs()
    {
        if (IsPlayersEnabled.Item1 && !IsPlayersEnabled.Item2 && !IsPlayersEnabled.Item3)
        {
            return ValidateEntries(DealerName, "DealerName") && ValidateEntries(Player1Name, "Player1Name");
        }
        if (IsPlayersEnabled.Item1 && IsPlayersEnabled.Item2 && !IsPlayersEnabled.Item3)
        {
            return ValidateEntries(DealerName, "DealerName") && ValidateEntries(Player1Name, "Player1Name") && ValidateEntries(Player2Name, "Player2Name");
        }
        if (IsPlayersEnabled.Item1 && IsPlayersEnabled.Item2 && IsPlayersEnabled.Item3)
        {
            return ValidateEntries(DealerName, "DealerName") && ValidateEntries(Player1Name, "Player1Name") && ValidateEntries(Player2Name, "Player2Name") && ValidateEntries(Player3Name, "Player3Name");
        }

        return false;

        // Method that validates the text entries
        bool ValidateEntries(string text, string errMsg)
        {
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show($"The {errMsg} value can't be empty", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
    }

    #endregion Commands

}
