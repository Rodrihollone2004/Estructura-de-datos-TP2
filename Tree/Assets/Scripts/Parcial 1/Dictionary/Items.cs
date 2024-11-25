using TMPro;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] TMP_Text textName;
    [SerializeField] TMP_Text textValue;

    string itemName;
    int itemValue;

    public string Name { get => itemName; set => itemName = value; }
    public int Value { get => itemValue; set => itemValue = value; }

    private void Awake()
    {
        itemValue = Random.Range(100, 250);
        itemName = GetName();
    }

    private void Start()
    {
        ShowValues();
    }

    public void ShowValues()
    {
        textValue.text = $"Value: {itemValue}";
    }

    public string GetName()
    {
        itemName = textName.text;

        return itemName;
    }
}
