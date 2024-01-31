namespace CardLibrary;

public class Hand
{
    /// <summary>
    /// A field that represents the deck object
    /// </summary>
    private Deck deck;

    /// <summary>
    /// A field that represents the cards
    /// </summary>
    private List<Card> cards = new List<Card>();

    /// <summary>
    /// A property that represents the cards
    /// </summary>
    public List<Card> Cards => cards;

    /// <summary>
    /// Constructor that creates a hand
    /// </summary>
    public Hand(Deck deck)
    {
        this.deck = deck;
        cards = new();
        Score = 0;
    }

    /// <summary>
    /// A property that represents the last card in the collection
    /// </summary>
    public Card LastCard { get; set; }

    /// <summary>
    /// A property that represents the nummber of cards in the collection
    /// </summary>
    public int NumberOfCards { get; set; }

    /// <summary>
    /// A property that represents the score of the hand
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Method that adds a new card
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(Card card)
    {
        if (card != null)
        {
            LastCard = card;
            cards.Add(card);
            NumberOfCards++;
            Score = SumOfCards();
        }
    }

    /// <summary>
    /// Method that adds two cards
    /// </summary>
    /// <param name="cList"></param>
    public void AddCards(List<Card> cList)
    {
        foreach (var card in cList)
        {
            if (card != null)
            {
                LastCard = card;
                cards.Add(card);
                NumberOfCards++;
                Score = SumOfCards();
            }
        }
    }

    /// <summary>
    /// Method that clears the hand
    /// </summary>
    public void Clear()
    {
        cards.Clear();
        LastCard = null;
        NumberOfCards = 0;
        Score = 0;
    }

    /// <summary>
    /// Method that sums the value of cards in the hand 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private int SumOfCards() =>
        cards.Sum(x => x.Value switch
        {
            Value.Two => 2,
            Value.Three => 3,
            Value.Four => 4,
            Value.Five => 5,
            Value.Six => 6,
            Value.Seven => 7,
            Value.Eight => 8,
            Value.Nine => 9,
            Value.Ten => 10,
            Value.Jack => 10,
            Value.Queen => 10,
            Value.King => 10,
            Value.Ace => 11,
            _ => throw new ArgumentException("Invalid card value"),
        });
}

