using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;

    [SerializeField] Transform contentParent;
    [SerializeField] GameObject itemPrefab;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            AddRandomItem();
        }

        DisplayItems();
    }

    public void DisplayItems()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        if (inventoryManager.items.Count > 0)
        {
            foreach (InventoryItem item in inventoryManager.items)
            {
                GameObject newItem = Instantiate(itemPrefab, contentParent);

                TextMeshProUGUI itemText = newItem.GetComponent<TextMeshProUGUI>();

                if (itemText != null)
                {
                    itemText.text = item.ToString();
                }
            }
        }
       
    }

    public void AddRandomItem()
    {
        string[] types = { "Spell", "Gem", "Amulet" };
        string itemType = types[Random.Range(0, types.Length)];

        float itemPrice = Random.Range(10, 200);
        string itemName;

        switch (itemType)
        {
            case "Spell":
                string[] spellNames = { "Fire", "Ice", "Bolt" };
                itemName = spellNames[Random.Range(0, spellNames.Length)];
                break;

            case "Gem":
                string[] gemNames = { "Ruby", "Diamond", "Emerald" };
                itemName = gemNames[Random.Range(0, gemNames.Length)];
                break;

            case "Amulet":
                string[] amuletNames = { "Talisman", "Necklace", "Ring" };
                itemName = amuletNames[Random.Range(0, amuletNames.Length)];
                break;

            default:
                itemName = "Unknown Item";
                break;
        }

        InventoryItem newItem = new InventoryItem(itemName, itemType, itemPrice);
        inventoryManager.AddItem(newItem);
        DisplayItems();
    }


    public void RemoveRandomItem()
    {
        inventoryManager.RemoveRandomItem();
        DisplayItems();
    }

    public void SortByName()
    {
        inventoryManager.SortByName();
        DisplayItems();
    }

    public void SortByType()
    {
        inventoryManager.SortByType(inventoryManager.items);
        DisplayItems();
    }

    public void SortByPrice()
    {
        inventoryManager.SortByPrice(inventoryManager.items);
        DisplayItems();
    }

}
