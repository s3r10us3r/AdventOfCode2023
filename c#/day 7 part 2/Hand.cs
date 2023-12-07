public class Hand
{
    public int[] cards;
    public int typeStrength = 0;
    public int bid = 0;
    public Hand(string handString, int bid)
    {
        this.bid = bid;

        cards = new int[5];
        for (int i = 0; i < 5; i++)
        {
            char card = handString[i];
            if (card >= '2' && card <= '9')
            {
                cards[i] = card - '0';
            }
            else
            {
                switch (card)
                {
                    case 'T':
                        cards[i] = 10;
                        break;
                    case 'J':
                        cards[i] = 1;
                        break;
                    case 'Q':
                        cards[i] = 12;
                        break;
                    case 'K':
                        cards[i] = 13;
                        break;
                    case 'A':
                        cards[i] = 14;
                        break;
                }
            }
        }
        DetermineType();
    }

    private void DetermineType()
    {
        var cardSets = new Dictionary<int, int>();
        int jokers = 0;
        foreach (int card in cards)
        {
            if (card == 1)
            {
                jokers++;
            }
            else if (!cardSets.TryAdd(card, 1))
                cardSets[card] += 1;
        }

        if(jokers == 5)
        {
            typeStrength = (int)Math.Pow(5, 5);
            return;
        }

        int highestValue = 0;
        int bestKey = 0;
        foreach(int key in cardSets.Keys)
        {
            if (cardSets[key] > highestValue)
            {
                highestValue = cardSets[key];
                bestKey = key;
            }
            else if(cardSets[key] == highestValue)
            {
                if(key > bestKey)
                    bestKey = key;
            }
        }
        
        cardSets[bestKey] += jokers;

        foreach (int value in cardSets.Values)
        {
            if(value != 1)
            {
                typeStrength += (int)Math.Pow(value, value);
            }
        }
    }
}

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand x, Hand y)
    {
        if (x.typeStrength - y.typeStrength != 0)
        {
            return x.typeStrength - y.typeStrength;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (x.cards[i] - y.cards[i] != 0)
                    return x.cards[i] - y.cards[i];
            }
        }
        return 0;
    }
}