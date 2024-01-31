namespace CardLibrary;

public class Player
{
    /// <summary>
    /// Constructor that creates a player
    /// </summary>
    /// <param name="name"></param>
    /// <param name="deck"></param>
    public Player(string name, Deck deck)
    {
        Name = name;
        Hand = new Hand(deck);
        IsFinished = false;
        IsWinner = false;
    }

    /// <summary>
    /// A property that represents the player's ID
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// A property that represents the player's hand
    /// </summary>
    public Hand Hand { get; set; }

    /// <summary>
    /// A property that represents the player's name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A property that represents if the player is finished
    /// </summary>
    public bool IsFinished { get; set; }

    /// <summary>
    /// A property that represents if the player is a winner
    /// </summary>
    public bool IsWinner { get; set; }

    /// <summary>
    /// Method that returns the current object
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Player: {Name}\nScore: {Hand.Score}\n";
}

