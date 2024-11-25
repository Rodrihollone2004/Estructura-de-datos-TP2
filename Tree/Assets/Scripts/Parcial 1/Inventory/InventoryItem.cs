using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string Name { get; set; }
    public string Type { get; set; }
    public float Price { get; set; }

    public InventoryItem(string name, string type, float price)
    {
        Name = name;
        Type = type;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Type} - ${Price}";
    }
}
