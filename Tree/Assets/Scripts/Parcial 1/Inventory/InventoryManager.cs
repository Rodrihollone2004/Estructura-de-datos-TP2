using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<InventoryItem> items;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            items = new List<InventoryItem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(InventoryItem newItem)
    {
        if (items.Count < 11)
        {
            items.Add(newItem);
        }
    }

    public void RemoveRandomItem()
    {
        if (items.Count > 0)
        {
            int randomIndex = Random.Range(0, items.Count);
            items.RemoveAt(randomIndex);
        }
    }

    public void SortByName()
    {
        for (int i = 0; i < items.Count - 1; i++)
        {
            for (int j = 0; j < items.Count - 1 - i; j++)
            {
                if (string.Compare(items[j].Name, items[j + 1].Name) > 0)
                {
                    InventoryItem temp = items[j];
                    items[j] = items[j + 1];
                    items[j + 1] = temp;
                }
            }
        }

    }

    public void SortByType(List<InventoryItem> items)
    {
        for (int i = 0; i < items.Count - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < items.Count; j++)
            {
                if (string.Compare(items[j].Type, items[minIndex].Type) < 0)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                InventoryItem temp = items[i];
                items[i] = items[minIndex];
                items[minIndex] = temp;
            }
        }
    }

    public void SortByPrice(List<InventoryItem> items)
    {
        List<InventoryItem> sortedItems = MergeSort(items);

        items.Clear();
        items.AddRange(sortedItems);
    }

    private List<InventoryItem> MergeSort(List<InventoryItem> items) //divide la lista de items en dos hasta que cada lista queda de 1 elemento
    {
        if (items.Count <= 1)
        {
            return items;
        }

        int mid = items.Count / 2;
        List<InventoryItem> left = items.GetRange(0, mid);
        List<InventoryItem> right = items.GetRange(mid, items.Count - mid);

        left = MergeSort(left);
        right = MergeSort(right);

        return Merge(left, right); 
    }

    private List<InventoryItem> Merge(List<InventoryItem> left, List<InventoryItem> right) //va juntando las listas de forma ordenada
    {
        List<InventoryItem> result = new List<InventoryItem>();
        int i = 0, j = 0; //dif indices para recorrer cada lista

        while (i < left.Count && j < right.Count) 
        {
            if (left[i].Price <= right[j].Price)
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        //se agrega el resto de los numeros a la lista
        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return result;
    }

    public void DisplayItems()
    {
        foreach (var item in items)
        {
            Debug.Log(item.ToString());
        }
    }
}


