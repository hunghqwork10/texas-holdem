using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public enum Round
    {
        PreFlop,
        Flop,
        Turn,
        River
    }

    public Round currentRound = Round.PreFlop;
    public List<Player> players;
    public Dictionary<Player, int> bets = new Dictionary<Player, int>();
    public int pot = 0;

    [SerializeField] Dealer dealer;

    public void Start()
    {
        // Set up the game
        dealer.Init();
        StartRound(Round.PreFlop);
    }

    public void StartRound(Round round)
    {
        currentRound = round;

        // Deal cards and update UI
        dealer.players = this.players;
        dealer.DealCards();
    }

    public void EndRound()
    {
        // Determine winner and award pot
        // Update UI
        // Start next round or end game
    }

    public void Bet(Player player, int amount)
    {
        if (bets.ContainsKey(player))
        {
            bets[player] += amount;
        }
        else
        {
            bets[player] = amount;
        }
        player.chips -= amount;
        pot += amount;
    }

    public void Fold(Player player)
    {
        // Remove player from the game
        // Update UI
    }
}
