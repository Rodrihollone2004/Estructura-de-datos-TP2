using UnityEngine;
using UnityEngine.UI;

public class ButtonsSelect : MonoBehaviour
{
    Items items;

    public enum buttonSelected
    {
        buttonHelmet,
        buttonSword,
        buttonShield
    }

    buttonSelected selectedButton;

    bool isHelmet;
    bool isSword;
    bool isShield;

    int helmetsAmount;
    int swordsAmount;
    int shieldsAmount;

    [SerializeField] Button buttonHelmet;
    [SerializeField] Button buttonSword;
    [SerializeField] Button buttonShield;

    DictionaryItems dictionaryItems;
    ItemsAmount itemsAmount;
    Coins coinsAmount;

    [SerializeField] Button buyButton;
    [SerializeField] Button saleButton;

    private void Awake()
    {
        dictionaryItems = FindObjectOfType<DictionaryItems>();
        itemsAmount = FindObjectOfType<ItemsAmount>();
        coinsAmount = FindObjectOfType<Coins>();

        buttonHelmet.onClick.AddListener(() => SelectButton(buttonSelected.buttonHelmet));
        buttonSword.onClick.AddListener(() => SelectButton(buttonSelected.buttonSword));
        buttonShield.onClick.AddListener(() => SelectButton(buttonSelected.buttonShield));

        buyButton.onClick.AddListener(BuyItem);
        saleButton.onClick.AddListener(SaleItem);
    }

    private void SelectButton(buttonSelected selectedButton)
    {
        switch (selectedButton)
        {
            case buttonSelected.buttonHelmet:
                items = buttonHelmet.GetComponentInParent<Items>();

                isHelmet = true;
                isSword = false;
                isShield = false;

                break;
            case buttonSelected.buttonSword:
                items = buttonSword.GetComponentInParent<Items>();

                isHelmet = false;
                isSword = true;
                isShield = false;

                break;
            case buttonSelected.buttonShield:
                items = buttonShield.GetComponentInParent<Items>();

                isHelmet = false;
                isSword = false;
                isShield = true;

                break;
            default:
                break;
        }
    }

    private void BuyItem()
    {
        if (isHelmet && !dictionaryItems.Items.ContainsKey(items.Name) && coinsAmount._Coins > items.Value)
        {
            helmetsAmount++;
            itemsAmount.SetAmountOfHelmets(helmetsAmount);
            dictionaryItems.AddItems(items);
            coinsAmount.SubstractCoins(items.Value);
        }
        if (isSword && !dictionaryItems.Items.ContainsKey(items.Name) && coinsAmount._Coins > items.Value)
        {
            swordsAmount++;
            itemsAmount.SetAmountOfSwords(swordsAmount);
            dictionaryItems.AddItems(items);
            coinsAmount.SubstractCoins(items.Value);
        }
        if (isShield && !dictionaryItems.Items.ContainsKey(items.Name) && coinsAmount._Coins > items.Value)
        {
            shieldsAmount++;
            itemsAmount.SetAmountOfShields(shieldsAmount);
            dictionaryItems.AddItems(items);
            coinsAmount.SubstractCoins(items.Value);
        }
    }

    private void SaleItem()
    {
        if (isHelmet && dictionaryItems.Items.ContainsKey(items.Name))
        {
            helmetsAmount--;
            itemsAmount.SetAmountOfHelmets(helmetsAmount);
            dictionaryItems.RemoveItems(items.Name);
            coinsAmount.AddCoins(items.Value);
        }

        if (isSword && dictionaryItems.Items.ContainsKey(items.Name))
        {
            swordsAmount--;
            itemsAmount.SetAmountOfSwords(swordsAmount);
            dictionaryItems.RemoveItems(items.Name);
            coinsAmount.AddCoins(items.Value);
        }

        if (isShield && dictionaryItems.Items.ContainsKey(items.Name))
        {
            shieldsAmount--;
            itemsAmount.SetAmountOfShields(shieldsAmount);
            dictionaryItems.RemoveItems(items.Name);
            coinsAmount.AddCoins(items.Value);
        }
    }
}
