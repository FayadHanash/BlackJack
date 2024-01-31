namespace CardLibrary;

public class Deck
{
    /// <summary>
    /// A field that represents the cards
    /// </summary>
    private ListManager<Card> cards;

    /// <summary>
    /// A field that represents the random object
    /// </summary>
    private Random Random = new Random();

    /// <summary>
    /// Default constructor that creates a deck of cards
    /// </summary>
    public Deck() : this(1)
    {
    }

    /// <summary>
    /// Constructor that creates a deck of cards
    /// </summary>
    /// <param name="numberOfDecks"></param>
    public Deck(int numberOfDecks)
    {
        InitializeDeck(numberOfDecks);
    }

    /// <summary>
    /// Method that initializes the deck
    /// </summary>
    /// <param name="numberOfDecks"></param>
    private void InitializeDeck(int numberOfDecks)
    {
        cards = new ListManager<Card>();
        for (int i = 1; i <= numberOfDecks; i++)
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    cards.Add(new Card(suit, value));
                }
            }
        }
    }

    /// <summary>
    /// A property that represents if the deck is empty
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    /// Method that adds a new card
    /// </summary>
    /// <param name="card"></param>
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    /// <summary>
    /// Method that deletes the cards
    /// </summary>
    public void DiscardCards()
    {
        cards.DeleteAll();
    }

    /// <summary>
    /// Method that returns a card at a given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Card GetAt(int index)
    {
        return cards.GetAt(index);
    }

    /// <summary>
    /// Method that returns a card randomly from the collection
    /// </summary>
    /// <returns></returns>
    public Card GetCard()
    {
        int index = Random.Next(0, cards.Count);
        var card = cards.GetAt(index);
        cards.DeleteAt(index);
        return card;
    }

    /// <summary>
    /// Method that returns two cards randomly from the collection
    /// </summary>
    /// <returns></returns>
    public List<Card> GetTwoCards()
    {
        List<Card> twoCards = new List<Card>();
        for (int i = 0; i < 2; i++)
        {
            int index = Random.Next(0, cards.Count);
            var card = cards.GetAt(index);
            cards.DeleteAt(index);
            twoCards.Add(card);
        }
        return twoCards;
    }

    /// <summary>
    /// Method that shuffles the collection
    /// </summary>
    public void Shuffle()
    {
        List<Card> shuffledCards = new List<Card>();
        while (cards.Count > 0)
        {
            int index = Random.Next(0, cards.Count);
            shuffledCards.Add(cards.GetAt(index));
            cards.DeleteAt(index);
        }
        cards = new ListManager<Card>(shuffledCards);
    }

    /// <summary>
    /// Method that returns number of cards in the collection
    /// </summary>
    /// <returns></returns>
    public int NumberOfCards()
    {
        return cards.Count;
    }

    /// <summary>
    /// Method that deletes a card at given index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCard(int index)
    {
        cards.DeleteAt(index);
    }

    /// <summary>
    /// Method that sums the values of cards
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public int SumOfCards()
    {
        int sum = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            sum += cards.GetAt(i).Value switch
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
            };
        }
        return sum;
    }
}
