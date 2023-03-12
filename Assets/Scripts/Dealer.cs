using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> communityCards;
    [SerializeField] public List<Player> players;

    private void Start()
    {
        
    }

    public void Init()
    {
        // init the deck
        this.deck = new List<Card>();
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(s, r));
            }
        }

        //ShuffleDeck(deck);

        // debug logging
        foreach (var card in deck)
        {
            Debug.Log(card.ToString());
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
}
