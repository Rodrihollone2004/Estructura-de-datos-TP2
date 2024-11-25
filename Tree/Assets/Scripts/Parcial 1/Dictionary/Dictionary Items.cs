using System.Collections.Generic;
using UnityEngine;

public class DictionaryItems : MonoBehaviour
{
    Dictionary<string, int> items = new Dictionary<string, int>();

    public Dictionary<string, int> Items { get => items; set => items = value; }

    public void AddItems(Items item)
    {
        items.Add(item.Name, item.Value);
    }

    public void RemoveItems(string key)
    {
        items.Remove(key);
    }
}
