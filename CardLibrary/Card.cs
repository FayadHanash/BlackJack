namespace CardLibrary;

public class Card
{
    /// <summary>
    /// Constructor that creates a card
    /// </summary>
    /// <param name="suit"></param>
    /// <param name="value"></param>
    public Card(Suit suit, Value value)
    {
        Suit = suit;
        Value = value;
    }

    /// <summary>
    /// A property that represents the card's suit
    /// </summary>
    public Suit Suit { get; set; }

    /// <summary>
    /// A property that represents the card's value
    /// </summary>
    public Value Value { get; set; }

    /// <summary>
    /// A property that represents the card's name/path 
    /// </summary>
    public string Image => $"{Suit}{Value}.png";

    /// <summary>
    /// Method that returns the current object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Value} of {Suit}";
    }

}

