using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HandEvaluator
{
    public static int Evaluate(List<Card> cards)
    {
        cards.Sort((a, b) => b.rank - a.rank);
        bool hasFlush = HasFlush(cards);
        bool hasStraight = HasStraight(cards);
        bool hasStraightFlush = hasFlush && hasStraight;
        bool hasFourOfAKind = HasFourOfAKind(cards);
        bool hasFullHouse = HasFullHouse(cards);
        bool hasThreeOfAKind = HasThreeOfAKind(cards);
        bool hasTwoPair = HasTwoPair(cards);
        bool hasPair = HasPair(cards);

        if (hasStraightFlush)
        {
            return 9;
        }
        else if (hasFourOfAKind)
        {
            return 8;
        }
        else if (hasFullHouse)
        {
            return 7;
        }
        else if (hasFlush)
        {
            return 6;
        }
        else if (hasStraight)
        {
            return 5;
        }
        else if (hasThreeOfAKind)
        {
            return 4;
        }
        else if (hasTwoPair)
        {
            return 3;
        }
        else if (hasPair)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private static bool HasFlush(List<Card> cards)
    {
        int hearts = 0;
        int diamonds = 0;
        int clubs = 0;
        int spades = 0;
        foreach (Card card in cards)
        {
            switch (card.suit)
            {
                case Suit.Hearts:
                    hearts++;
                    break;
                case Suit.Diamonds:
                    diamonds++;
                    break;
                case Suit.Clubs:
                    clubs++;
                    break;
                case Suit.Spades:
                    spades++;
                    break;
            }
        }
        return hearts >= 5 || diamonds >= 5 || clubs >= 5 || spades >= 5;
    }

    private static bool HasStraight(List<Card> cards)
    {
        int numConsecutive = 1;
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].rank == cards[i + 1].rank + 1)
            {
                numConsecutive++;
                if (numConsecutive >= 5)
                {
                    return true;
                }
            }
            else if (cards[i].rank != cards[i + 1].rank)
            {
                numConsecutive = 1;
            }
        }
        // check special case of Ace, 5, 4, 3, 2
        if (numConsecutive == 4 && cards[0].rank == Rank.Ace && cards[cards.Count - 1].rank == Rank.Two)
        {
            return true;
        }
        return false;
    }

    private static bool HasFourOfAKind(List<Card> cards)
    {
        for (int i = 0; i <= cards.Count - 4; i++)
        {
            if (cards[i].rank == cards[i + 1].rank &&
                cards[i].rank == cards[i + 2].rank &&
                cards[i].rank == cards[i + 3].rank)
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasFullHouse(List<Card> cards)
    {
        int count = 1;
        Rank rank1 = cards[0].rank;
        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i].rank == rank1)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        if (count == 3)
        {
            if (cards[3].rank == cards[4].rank)
            {
                return true;
            }
        }
        else if (count == 2)
        {
            if (cards[2].rank == cards[3].rank &&
                cards[3].rank == cards[4].rank)
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasThreeOfAKind(List<Card> cards)
    {
        for (int i = 0; i <= cards.Count - 3; i++)
        {
            if (cards[i].rank == cards[i + 1].rank &&
                cards[i].rank == cards[i + 2].rank)
            {
                return true;
            }
        }
        return false;
    }

    private static bool HasTwoPair(List<Card> cards)
    {
        int numPairs = 0;
        for (int i = 0; i <= cards.Count - 2; i++)
        {
            if (cards[i].rank == cards[i + 1].rank)
            {
                numPairs++;
                i++;
            }
        }
        return numPairs >= 2;
    }

    private static bool HasPair(List<Card> cards)
    {
        for (int i = 0; i <= cards.Count - 2; i++)
        {
            if (cards[i].rank == cards[i + 1].rank)
            {
                return true;
            }
        }
        return false;
    }

}