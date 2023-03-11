using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    public static int NUM_OF_PLAYERS = 6;

    public enum HandRanking
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public HandRanking GetHandRanking(List<Card> hand, List<Card> communityCards)
    {
        List<Card> allCards = new List<Card>(hand);
        allCards.AddRange(communityCards);
        allCards.Sort((a, b) => a.rank - b.rank);

        bool isFlush = IsFlush(allCards);
        bool isStraight = IsStraight(allCards);
        bool isStraightFlush = isFlush && isStraight;

        if (isStraightFlush)
        {
            if (allCards[0].rank == Rank.Ace)
            {
                return HandRanking.RoyalFlush;
            }
            else
            {
                return HandRanking.StraightFlush;
            }
        }
        else if (HasNOfAKind(allCards, 4))
        {
            return HandRanking.FourOfAKind;
        }
        else if (HasFullHouse(allCards))
        {
            return HandRanking.FullHouse;
        }
        else if (isFlush)
        {
            return HandRanking.Flush;
        }
        else if (isStraight)
        {
            return HandRanking.Straight;
        }
        else if (HasNOfAKind(allCards, 3))
        {
            return HandRanking.ThreeOfAKind;
        }
        else if (HasTwoPair(allCards))
        {
            return HandRanking.TwoPair;
        }
        else if (HasNOfAKind(allCards, 2))
        {
            return HandRanking.Pair;
        }
        else
        {
            return HandRanking.HighCard;
        }
    }

    private bool IsFlush(List<Card> cards)
    {
        int count = 0;
        Suit suit = cards[0].suit;
        foreach (Card card in cards)
        {
            if (card.suit == suit)
            {
                count++;
            }
            else
            {
                suit = card.suit;
                count = 1;
            }
            if (count >= 5)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsStraight(List<Card> cards)
    {
        int count = 1;
        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i].rank == cards[i - 1].rank + 1)
            {
                count++;
                if (count >= 5)
                {
                    return true;
                }
            }
            else if (cards[i].rank != cards[i - 1].rank)
            {
                count = 1;
            }
        }
        return false;
    }

    private bool HasNOfAKind(List<Card> cards, int n)
    {
        int count = 1;
        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i].rank == cards[i - 1].rank)
            {
                count++;
                if (count >= n)
                {
                    return true;
                }
            }
            else
            {
                count = 1;
            }
        }
        return false;
    }

    private bool HasFullHouse(List<Card> cards)
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
        if (count < 3)
        {
            return false;
        }
        Rank rank2 = cards[count].rank;
        if (rank1 != rank2)
        {
            int temp = count;
            count = 1;
            rank1 = rank2;
            for (int i = temp + 1; i < cards.Count; i++)
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
            if (count < 2)
            {
                return false;
            }
        }
        return true;
    }

    private bool HasTwoPair(List<Card> cards)
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
                rank1 = cards[i].rank;
                if (count >= 2)
                {
                    break;
                }
                count = 1;
            }
        }
        if (count < 2)
        {
            return false;
        }
        Rank rank2 = Rank.Two;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].rank != rank1 && cards[i].rank == rank2)
            {
                return true;
            }
            else if (cards[i].rank != rank1)
            {
                rank2 = cards[i].rank;
            }
        }
        return false;
    }
}
