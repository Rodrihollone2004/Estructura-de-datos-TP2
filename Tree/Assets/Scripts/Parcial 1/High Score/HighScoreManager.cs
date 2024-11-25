using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private List<HighscorePlayer> players = new List<HighscorePlayer>();
    [SerializeField] private HighscorePlayer playerPrefab;

    [SerializeField] private int maxItems;
    [SerializeField] private Transform contentParent;

    void Start()
    {
        for (int i = 0; i < maxItems; i++)
        {
            string playerId = (i + 1).ToString();
            string playerName = "Player " + (i + 1).ToString();
            int playerScore = Random.Range(0, 1000);

            AddPlayer(playerId, playerName, playerScore);
        }

    }

    public void OrderHighScore()
    {
        BubbleSort(players, false);
    }

    public void OrderLowScore()
    {
        BubbleSort(players, true);
    }

    void BubbleSort(List<HighscorePlayer> list, bool ascendent)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                bool swap = false;

                if (ascendent)
                    swap = list[j].Score > list[j + 1].Score;
                else
                    swap = list[j].Score < list[j + 1].Score;

                if (swap)
                {
                    HighscorePlayer temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }

        ShowList();
    }

    private void ShowList()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.SetSiblingIndex(i);
            players[i].SetValues((i + 1).ToString(), players[i].Name, players[i].Score);
        }
    }

    public void AddPlayer(string id, string name, int score)
    {
        HighscorePlayer player = Instantiate(playerPrefab, contentParent);
        player.SetValues(id, name, score);

        players.Add(player);
    }

}
