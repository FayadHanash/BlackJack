namespace CardLibrary;

public class Engine
{
    /// <summary>
    /// A field that represents the players
    /// </summary>
    List<Player> players = new List<Player>();

    /// <summary>
    /// A field that represents the dealer
    /// </summary>
    private Player dealer;

    /// <summary>
    /// A field that represents the nummber of decks
    /// </summary>
    private int numberOfDecks;

    /// <summary>
    /// A field that represents the deck
    /// </summary>
    private Deck deck;

    /// <summary>
    /// A property that represents the dealer
    /// </summary>
    public Player Dealer => dealer;

    /// <summary>
    /// A property that represents the players
    /// </summary>
    public List<Player> Players => players;

    /// <summary>
    /// Constructor that initializes the engine
    /// </summary>
    public Engine() : this(1)
    {
    }

    /// <summary>
    /// Constructor that initializes the engine
    /// </summary>
    /// <param name="numberOfDecks"></param>
    public Engine(int numberOfDecks)
    {
        players = new List<Player>();
        this.numberOfDecks = numberOfDecks;
        deck = new Deck(numberOfDecks);
        deck.Shuffle();
    }

    /// <summary>
    /// Method that shuffles the cards
    /// </summary>
    public void Shuffle()
    {
        deck = new Deck(numberOfDecks);
        deck.Shuffle();
    }

    /// <summary>
    /// Method that initializes the players
    /// </summary>
    /// <param name="name"></param>
    public void InitializePlayer(string name)
    {
        players.Add(new Player(name, deck));
        players.Last().Hand.AddCards(deck.GetTwoCards());
    }

    /// <summary>
    /// Method that initializes the dealer
    /// </summary>
    /// <param name="name"></param>
    public void InitializeDealer(string name)
    {
        dealer = new Player(name, deck);
        dealer.Hand.AddCard(deck.GetCard());
    }

    /// <summary>
    /// Method that adds a card to the player's hand and checks the winner
    /// </summary>
    /// <param name="player"></param>
    public void PlayerHit(int player)
    {
        players[player].Hand.AddCard(deck.GetCard());
        if (players[player].Hand.Score > 21)
        {
            int aceCount = players[player].Hand.Cards.Where(x => x.Value == Value.Ace).Count();
            while (aceCount > 0 && players[player].Hand.Score > 21)
            {
                players[player].Hand.Score -= 10;
                aceCount--;
            }
        }

        if (players[player].Hand.Score <= 21 && players[player].Hand.Score > dealer.Hand.Score)
        {
            players[player].IsWinner = true;
            players[player].IsFinished = true;
        }
        else if (players[player].Hand.Score > 21)
        {
            players[player].IsFinished = true;
            players[player].IsWinner = false;
        }

    }

    /// <summary>
    /// Method that adds a card to the dealer's hand and checks the winner
    /// </summary>
    public void DealerHit()
    {
        dealer.Hand.AddCard(deck.GetCard());
        if (dealer.Hand.Score > 21)
        {
            int aceCount = dealer.Hand.Cards.Where(x => x.Value == Value.Ace).Count();
            while (aceCount > 0 && dealer.Hand.Score > 21)
            {
                dealer.Hand.Score -= 10;
                aceCount--;
            }

            if (dealer.Hand.Score > 21)
            {
                dealer.IsFinished = true;
                dealer.IsWinner = false;
            }
            else
            {
                dealer.IsFinished = true;
                bool isWinner = true;
                foreach (var player in players)
                {
                    if (!player.IsFinished || player.Hand.Score > dealer.Hand.Score)
                    {
                        isWinner = false;
                        break;
                    }
                }
                dealer.IsWinner = isWinner;
            }
        }
    }
}
