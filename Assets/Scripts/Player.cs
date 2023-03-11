using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
    public List<Card> hand;
    public int chips;
    public int bet;

    public Player(string name, int chips)
    {
        this.name = name;
        this.chips = chips;
        hand = new List<Card>();
        bet = 0;
    }

    public void AddCardToHand(Card card)
    {
        hand.Add(card);
    }

    public void ClearHand()
    {
        hand.Clear();
    }

    public int GetHandValue(List<Card> communityCards)
    {
        List<Card> allCards = new List<Card>(hand);
        allCards.AddRange(communityCards);
        return HandEvaluator.Evaluate(allCards);
    }

    public int GetMaxBet(int minBet)
    {
        return chips + bet - minBet;
    }

    public void Bet(int amount)
    {
        chips -= amount;
        bet += amount;
    }

    public void Win(int pot)
    {
        chips += pot;
    }

    public bool IsAllIn(int minBet)
    {
        return chips + bet <= minBet;
    }

    public override string ToString()
    {
        string handStr = "";
        foreach (Card card in hand)
        {
            handStr += card.ToString() + " ";
        }
        return name + ": " + handStr + "(" + chips + ")";
    }
}
