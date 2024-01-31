using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media.Imaging;
using CardLibrary;
using System.Windows;
using BlackJack.Services;
using Logger;


namespace BlackJack.ViewModels;

/// <summary>
/// The ViewModel for the MainView
/// </summary>
public partial class MainViewModel : ObservableObject
{
    #region Fields

    /// <summary>
    /// Field that represents the Engine object
    /// </summary>
    private Engine _engine;

    /// <summary>
    /// Field that represents the number of players
    /// </summary>
    private int NumberOfPlayers = 3;

    /// <summary>
    /// Field that represents if the dealer has to hit 
    /// </summary>
    private (bool, bool, bool, bool) IsDealerTurn = new();

    /// <summary>
    /// Field that represents the data which will be received from the newWindow
    /// </summary>
    private Dictionary<string, object> Data = new();

    /// <summary>
    /// Field that represents the logger object
    /// </summary>
    private ILogger _logger;

    /// <summary>
    /// Field that represents the NewWindowService object
    /// </summary>
    private readonly INewWindowService _windowService = new NewWindowService();

    #endregion Fields

    #region Properties
    /// <summary>
    /// Observable property that represents the title and author of the application
    /// </summary>
    [ObservableProperty]
    private string author;

    /// <summary>
    /// Observable property that represents the width of the window
    /// </summary>
    [ObservableProperty]
    private double windowWidth;

    /// <summary>
    /// Observable property that represents the dealer
    /// </summary>
    [ObservableProperty]
    private PlayerViewModel dealer;

    /// <summary>
    /// Observable property that represents the players
    /// </summary>
    [ObservableProperty]
    private List<PlayerViewModel> players;

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Main constructor
    /// </summary>
    public MainViewModel()
    {
        _logger = new Logger.Logger();
        WindowWidth = 600;
        Author = "BlackJack By Fayad Al Hanash";
        InitializePlayers();
    }

    #endregion Constructors

    #region Methods
    /// <summary>
    /// Method that initializes the window
    /// </summary>
    private void InitializeWindow()
    {
        Dealer = new();
        Players = new()
        {
            { new() },
            { new() },
            { new() },
        };

        JustifyPlayerLayout();
        IsDealerTurn = (false, false, false, false);
    }

    private void ResizeWindow(int width, int height)
    {
        var app = Application.Current as App;
        app.ResizeWindow(width, height);
    }

    /// <summary>
    /// Method that initializes the players and the dealer 
    /// </summary>
    private void InitializePlayers()
    {

        int NumOfDecks = Data.Count > 0 ? (int)Data["NumberOfDecks"] : 1;
        string DealerName = Data.Count > 0 ? (string)Data["DealerName"] : "Dealer";
        NumberOfPlayers = Data.Count > 0 ? (int)Data["NumberOfPlayers"] : NumberOfPlayers;
        switch (NumberOfPlayers)
        {
            case 1:
                ResizeWindow(400, 600);
                break;
            case 2:
                ResizeWindow(800, 600);
                break;
            case 3:
                ResizeWindow(1200, 600);
                break;
        }
        _engine = new(NumOfDecks);
        _engine.InitializeDealer(DealerName);

        _logger.LogMessage($"A new game has been started with {NumOfDecks} decks and with {NumberOfPlayers} players");
        _logger.LogMessage($"Dealer name: {DealerName}");

        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            string PlayerName = Data.Count > 0 ? (string)Data[$"Player{i}Name"] : $"Player{i}";
            _engine.InitializePlayer(PlayerName);
            _logger.LogMessage($"Player {i} name: {PlayerName}");
        }
        InitializeWindow();
        Update();

    }

    /// <summary>
    /// Method that adjusts plyers layout to be presented 
    /// </summary>
    private void JustifyPlayerLayout()
    {
        if (NumberOfPlayers == 1)
        {
            WindowWidth = 400;
            Players[(int)PlayerNum.Player1].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player1].HorizontalAlignment = HorizontalAlignment.Center;
            Players[(int)PlayerNum.Player2].Visibility = Visibility.Hidden;
            Players[(int)PlayerNum.Player3].Visibility = Visibility.Hidden;
        }
        else if (NumberOfPlayers == 2)
        {
            WindowWidth = 800;
            Players[(int)PlayerNum.Player1].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player2].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player3].Visibility = Visibility.Hidden;
            Players[(int)PlayerNum.Player1].HorizontalAlignment = HorizontalAlignment.Left;
            Players[(int)PlayerNum.Player2].HorizontalAlignment = HorizontalAlignment.Right;
        }
        else if (NumberOfPlayers == 3)
        {
            WindowWidth = 1000;
            Players[(int)PlayerNum.Player1].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player2].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player3].Visibility = Visibility.Visible;
            Players[(int)PlayerNum.Player1].HorizontalAlignment = HorizontalAlignment.Left;
            Players[(int)PlayerNum.Player2].HorizontalAlignment = HorizontalAlignment.Center;
            Players[(int)PlayerNum.Player3].HorizontalAlignment = HorizontalAlignment.Right;
        }
    }


    /// <summary>
    /// Method that adjusts cards to be presented
    /// </summary>
    private void UpdateCards()
    {
        Dealer.Cards.Clear();
        Players.ForEach(p => p.Cards.Clear());
        _engine.Dealer.Hand.Cards.ForEach(c => Dealer.Cards.Add(new BitmapImage(new Uri($"../Images/{c.Image}", UriKind.Relative))));

        for (int i = 0; i < _engine.Players.Count; i++)
        {
            _engine.Players[i].Hand.Cards.ForEach(c => Players[i].Cards.Add(new BitmapImage(new Uri($"../Images/{c.Image}", UriKind.Relative))));
        }
    }

    /// <summary>
    /// Method that updates the GUI by calling ManagePlayerButtons, UpdatePlayerInfo, and UpdateCards methods.
    /// </summary>
    private void Update()
    {
        ManagePlayerButtons();
        UpdatePlayerInfo();
        UpdateCards();
    }

    /// <summary>
    /// Method that enables/disables the player' buttons.
    /// </summary>
    private void ManagePlayerButtons()
    {
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            bool isFinished = _engine.Players[i].IsFinished;
            switch (i)
            {
                case (int)PlayerNum.Player1:
                    Players[i].IsButtonsEnabled = !isFinished;
                    break;
                case (int)PlayerNum.Player2:
                    Players[i].IsButtonsEnabled = !isFinished;
                    break;
                case (int)PlayerNum.Player3:
                    Players[i].IsButtonsEnabled = !isFinished;
                    break;
            }
        }

        if (_engine.Dealer.IsFinished)
        {
            Players.ForEach(p => p.IsButtonsEnabled = false);
        }
    }

    /// <summary>
    /// Method that updates/displays the player' informantion such as the plyer name, score...
    /// </summary>
    private void UpdatePlayerInfo()
    {
        Player dealer = _engine.Dealer;
        Dealer.Info = dealer.ToString();
        _ = dealer.IsFinished ? dealer.IsWinner ? Dealer.Info += "Winner" : Dealer.Info += "Bust" : "";

        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Player player = _engine.Players[i];
            string playerInfo = player.ToString();
            if (player.IsFinished)
            {
                playerInfo += player.IsWinner ? "Winner" : "Bust";
            }
            switch (i)
            {
                case (int)PlayerNum.Player1:
                    Players[i].Info = playerInfo;
                    break;
                case (int)PlayerNum.Player2:
                    Players[i].Info = playerInfo;
                    break;
                case (int)PlayerNum.Player3:
                    Players[i].Info = playerInfo;
                    break;
            }
        }
    }

    /// <summary>
    /// Method that manages the dealer's action, such has the dealer to hit? 
    /// </summary>
    /// <param name="dealer"></param>
    /// <param name="isPlayer"></param>
    /// <param name="player"></param>
    private void ManageTurns(bool dealer, PlayerNum isPlayer, bool player)
    {
        IsDealerTurn.Item1 = dealer;
        switch (isPlayer)
        {
            case PlayerNum.Player1:
                IsDealerTurn.Item2 = player;
                Players[(int)PlayerNum.Player1].IsButtonsEnabled = false;
                break;
            case PlayerNum.Player2:
                IsDealerTurn.Item3 = player;
                Players[(int)PlayerNum.Player2].IsButtonsEnabled = false;
                break;
            case PlayerNum.Player3:
                IsDealerTurn.Item4 = player;
                Players[(int)PlayerNum.Player3].IsButtonsEnabled = false;
                break;
        }

        DealerHit();
    }

    /// <summary>
    /// Method that allows the dealer to hit and add a new card 
    /// Calls the Update method
    /// </summary>
    private void DealerHit()
    {
        switch (NumberOfPlayers)
        {
            case 1:
                if (IsDealerTurn.Item1 && IsDealerTurn.Item2)
                {
                    common();
                }
                break;
            case 2:
                if (IsDealerTurn.Item1 && IsDealerTurn.Item2 && IsDealerTurn.Item3)
                {
                    Players[(int)PlayerNum.Player2].IsButtonsEnabled = true;
                    common();
                }
                break;
            case 3:
                if (IsDealerTurn.Item1 && IsDealerTurn.Item2 && IsDealerTurn.Item3 && IsDealerTurn.Item4)
                {
                    Players[(int)PlayerNum.Player2].IsButtonsEnabled = true;
                    Players[(int)PlayerNum.Player3].IsButtonsEnabled = true;
                    common();
                }
                break;
        }

        // A local function that is used to reduce code duplication
        void common()
        {
            _engine.DealerHit();
            IsDealerTurn = (false, false, false, false);
            Players[(int)PlayerNum.Player1].IsButtonsEnabled = true;
            Update();
        }
    }

    /// <summary>
    /// Method that allows the players to hit
    /// Calls the ManageTurns and Update methods
    /// </summary>
    /// <param name="playerNum"></param>
    private void PlayerHit(PlayerNum playerNum)
    {
        _engine.PlayerHit((int)playerNum);
        ManageTurns(true, playerNum, true);
        Update();
        _logger.LogMessage($"Player {(int)playerNum + 1} has hit");
    }

    /// <summary>
    /// Method that allwos the players to stand 
    /// Calls the ManageTurns method 
    /// </summary>
    /// <param name="playerNum"></param>
    private void PlayerStand(PlayerNum playerNum)
    {
        ManageTurns(true, playerNum, true);
        _logger.LogMessage($"Player {(int)playerNum + 1} has stood");
    }

    #endregion Methods

    #region Commands

    /// <summary>
    /// Method that indicates player 1 has clicked the hit button
    /// </summary>
    [RelayCommand]
    private void Player1Hit()
    {
        PlayerHit(PlayerNum.Player1);
    }

    /// <summary>
    /// Method that indicates player 1 has clicked the stand button
    /// </summary>
    [RelayCommand]
    private void PLayer1Stand()
    {
        PlayerStand(PlayerNum.Player1);
    }

    /// <summary>
    /// Method that indicates player 2 has clicked the hit button
    /// </summary>
    [RelayCommand]
    private void Player2Hit()
    {
        PlayerHit(PlayerNum.Player2);
    }

    /// <summary>
    /// Method that indicates player 2 has clicked the stand button
    /// </summary>
    [RelayCommand]
    private void PLayer2Stand()
    {
        PlayerStand(PlayerNum.Player2);
    }

    /// <summary>
    /// Method that indicates player 3 has clicked the hit button
    /// </summary>
    [RelayCommand]
    private void Player3Hit()
    {
        PlayerHit(PlayerNum.Player3);
    }

    /// <summary>
    /// Method that indicates player 3 has clicked the stand button
    /// </summary>
    [RelayCommand]
    private void PLayer3Stand()
    {
        PlayerStand(PlayerNum.Player3);
    }

    /// <summary>
    /// Method that indicates NewGame button has been clicked
    /// </summary>
    [RelayCommand]
    private void NewGame()
    {
        _logger.LogMessage($"New Game has started");
        var newViewModel = new NewViewModel(_windowService);
        var data = _windowService.ShowWindow(newViewModel);
        if (data != null)
        {
            Data = new();
            Data = data;
            InitializePlayers();
        }
    }

    /// <summary>
    /// Method that indicates Refresh button has been clicked
    /// </summary>
    [RelayCommand]
    private void Refresh()
    {
        _logger.LogMessage($"Refresh has been clicked");
        InitializePlayers();

    }

    /// <summary>
    /// Method that indicates Shuffle button has been clicked
    /// </summary>
    [RelayCommand]
    private void Shuffle()
    {
        InitializeWindow();

        _logger.LogMessage($"Shuffle has been clicked");
        int dealerCards = _engine.Dealer.Hand.Cards.Count;
        _engine.Dealer.Hand.Cards.Clear();

        int[] temp = new int[NumberOfPlayers];
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            temp[i] = _engine.Players[i].Hand.Cards.Count;
            _engine.Players[i].Hand.Cards.Clear();
        }

        _engine.Shuffle();

        for (int i = 0; i < dealerCards; i++)
        {
            DealerHit();
        }

        for (int i = 0; i < NumberOfPlayers; i++)
        {
            for (int j = 0; j < temp[i]; j++)
            {
                PlayerHit((PlayerNum)i);
            }
        }

        Update();
    }


    #endregion Commands
}

