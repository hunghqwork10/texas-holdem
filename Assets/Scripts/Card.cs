using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public class Card
{
    public Suit suit;
    public Rank rank;
    public bool isHidden;

    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        this.isHidden = false;
    }

    public string GetCardString()
    {
        string rankStr;
        switch (rank)
        {
            case Rank.Ten:
                rankStr = "T";
                break;
            case Rank.Jack:
                rankStr = "J";
                break;
            case Rank.Queen:
                rankStr = "Q";
                break;
            case Rank.King:
                rankStr = "K";
                break;
            case Rank.Ace:
                rankStr = "A";
                break;
            default:
                rankStr = ((int)rank).ToString();
                break;
        }

        string suitStr = suit.ToString().ToLower().Substring(0, 1);
        return rankStr + suitStr;
    }

    public string GetHiddenString()
    {
        return "X";
    }

    public override string ToString()
    {
        if (isHidden)
        {
            return GetHiddenString();
        }

        return GetCardString();
    }
}
