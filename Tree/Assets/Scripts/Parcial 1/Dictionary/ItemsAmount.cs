using TMPro;
using UnityEngine;

public class ItemsAmount : MonoBehaviour
{
    [SerializeField] TMP_Text textHelmetsAmount;
    [SerializeField] TMP_Text textSwordsAmount;
    [SerializeField] TMP_Text textShieldsAmount;

    private void Start()
    {
        SetAmountOfHelmets(0);
        SetAmountOfSwords(0);
        SetAmountOfShields(0);
    }

    public void SetAmountOfHelmets(int helmetsAmount)
    {
        textHelmetsAmount.text = $"{helmetsAmount} / 1"; ;
    }

    public void SetAmountOfSwords(int swordsAmount)
    {
        textSwordsAmount.text = $"{swordsAmount} / 1";
    }

    public void SetAmountOfShields(int shieldsAmount)
    {
        textShieldsAmount.text = $"{shieldsAmount} / 1"; ;
    }
}
