using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    public List<Card> deck;
    public List<Card> communityCards;
    public List<Player> players;

    void DealCards()
    {
        ShuffleDeck(deck);
        for (int i = 0; i < 2; i++)
        {
            foreach (Player player in players)
            {
                player.hand.Add(deck[0]);
                deck.RemoveAt(0);
            }
        }
    }

    void ShuffleDeck(List<Card> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
