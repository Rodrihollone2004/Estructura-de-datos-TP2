using TMPro;
using UnityEngine;

public class HighscorePlayer : MonoBehaviour
{
    [SerializeField] TMP_Text idText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text scoreText;

    public string Name { get; private set; }
    public string ID { get; private set; }
    public int Score { get; private set; }

    public void SetValues(string id, string name, int score)
    {
        Name = name;
        ID = id;
        Score = score;

        idText.text = id;
        nameText.text = name;
        scoreText.text = score.ToString();
    }

}
