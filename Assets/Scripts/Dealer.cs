using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public List<Card> deck;
    private List<Card> communityCards = new List<Card>();
    [SerializeField] public List<Player> players;

    public List<Card> CommunityCards
    {
        get => communityCards;
        set => communityCards = value;
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        // init the deck
        InitNewDeck();

        //// debug logging
        //foreach (var card in deck)
        //{
        //    Debug.Log(card.ToString());
        //}
    }

    public void InitNewDeck()
    {
        this.deck = new List<Card>();
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(s, r));
            }
        }
    }

    public void DealCards()
    {
        ShuffleDeck(deck);
        for (int i = 0; i < 2; i++)
        {
            foreach (Player player in players)
            {
                player.AddCardToHand(deck[0]);
                deck.RemoveAt(0);
            }
        }
    }

    void ShuffleDeck(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void ClearCommunityCards()
    {
        this.communityCards.Clear();
    }

    // Deal the flop (three community cards)
    public void DealFlop()
    {
        for (int i = 0; i < 3; i++)
        {
            communityCards.Add(DealCardPostFlop());
        }
    }

    // Deal the turn (one more community card)
    public void DealTurn()
    {
        communityCards.Add(DealCardPostFlop());
    }

    // Deal the river (one more community card)
    public void DealRiver()
    {
        communityCards.Add(DealCardPostFlop());
    }

    // Deal a card from the deck
    private Card DealCardPostFlop()
    {
        Card card = deck[0];
        deck.RemoveAt(0);
        return card;
    }
}
