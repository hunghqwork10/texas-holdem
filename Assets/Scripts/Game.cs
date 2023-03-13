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
    [SerializeField] TableView tableView;

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
        switch (round)
        {
            case Round.PreFlop:
                dealer.players = this.players;
                dealer.DealCards();
                break;
            case Round.Flop:
                dealer.DealFlop();
                string communityCardString = string.Empty;
                foreach(var card in dealer.CommunityCards)
                {
                    communityCardString += card.ToString();
                }
                tableView.communityCardText.text = communityCardString;
                break;
            case Round.Turn:
                dealer.DealTurn();
                tableView.communityCardText.text += dealer.CommunityCards[3].ToString();
                break;
            case Round.River:
                dealer.DealRiver();
                tableView.communityCardText.text += dealer.CommunityCards[4].ToString();
                break;
            default:
                break;
        }
    }

    public void EndRound()
    {
        // Determine winner and award pot
        Player winner = DetermineWinner();
        winner.chips += pot;
        pot = 0;

        // Update UI
        UpdateUI();

        // Start next round or end game
        if (players.Count > 1)
        {
            StartRound(GetNextRound(currentRound));
        }
        else
        {
            EndGame();
        }
    }

    public void NextRound()
    {
        StartRound(GetNextRound(currentRound));
    }

    private Player DetermineWinner()
    {
        // Implement logic for determining the winner based on the players' hands and the community cards.

        // Return the first player for demonstration purposes.
        return players[0];
    }

    private void UpdateUI()
    {
        // Implement code for updating the UI based on the current state of the game.
        // This could involve updating the player's chips, the pot size, the community cards, etc.
    }

    private Round GetNextRound(Round currentRound)
    {
        // Determine the next round based on the current round.
        switch (currentRound)
        {
            case Round.PreFlop:
                return Round.Flop;
            case Round.Flop:
                return Round.Turn;
            case Round.Turn:
                return Round.River;
            case Round.River:
                return Round.PreFlop;
            default:
                return Round.PreFlop;
        }
    }

    public void EndGame()
    {
        // Implement code for ending the game and displaying the winner, etc.
    }

    public void StartNextGame()
    {
        // Reset the game
        currentRound = Round.PreFlop;
        bets.Clear();
        tableView.ClearCommunityCardTextView();
        dealer.ClearCommunityCards();
        pot = 0;
        dealer.InitNewDeck();

        // Reset each player's hand and chips
        foreach (Player player in players)
        {
            player.Reset();
        }

        // Start a new round
        StartRound(currentRound);
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
