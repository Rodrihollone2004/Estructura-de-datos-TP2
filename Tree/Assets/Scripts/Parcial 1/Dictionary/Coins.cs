using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] TMP_Text textCoinsAmount;
    public int _Coins { get; private set; }

    private void Start()
    {
        _Coins = Random.Range(1000, 2000);
        textCoinsAmount.text = $"Coins: {_Coins}";
    }

    public void AddCoins(int objectValue)
    {
        _Coins += objectValue;
        textCoinsAmount.text = $"Coins: {_Coins}";
    }

    public void SubstractCoins(int objectValue)
    {
        _Coins -= objectValue;
        textCoinsAmount.text = $"Coins: {_Coins}";
    }
}
