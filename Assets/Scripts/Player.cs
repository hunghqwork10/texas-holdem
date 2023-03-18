using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public string name;
    public bool isLocalPlayer;
    public List<Card> hand;
    public TextMeshProUGUI handText;
    public int chips;
    public int startingChips = 1000;
    public int bet;

    public Player(string name, int chips)
    {
        this.name = name;
        this.chips = chips;
        hand = new List<Card>();
        bet = 0;
    }

    private void Start()
    {
       
    }

    private void OnEnable()
    {
        hand = new List<Card>();
        bet = 0;
    }

    public void AddCardToHand(Card card)
    {
        hand.Add(card);
        handText.text += card.ToString();

    }

    public void ShowHiddenHand()
    {
        handText.text = string.Empty;
        foreach(var card in hand)
        {
            handText.text += card.GetCardString();
        }
    }

    public string GetHandString()
    {
        string handString = string.Empty;
        foreach (var card in hand)
        {
            handString += card.GetCardString();
        }

        return handString;
    }

    public void Reset()
    {
        ClearHand();
        //chips = startingChips;
    }

    public void ClearHand()
    {
        hand.Clear();
        handText.text = string.Empty;
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
