using System.Collections.Generic;
using UnityEngine;

public class Ordering : MonoBehaviour
{
    [SerializeField] List<Items> items;

    private void Start()
    {
        CompareSelectionSort();
    }

    public void CompareSelectionSort()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int minValue = GetNumOfMinValue(items, i);

            if(items[i].Value > items[minValue].Value)
            {
                Vector3 tempPosition = items[i].transform.position;
                items[i].transform.position = items[minValue].transform.position;
                items[minValue].transform.position = tempPosition;

                Items temItemPos = items[i];
                items[i] = items[minValue];
                items[minValue] = temItemPos;
            }
        }
    }

    private int GetNumOfMinValue(List<Items> items, int startIndex)
    {
        int outputIndex = startIndex;

        for (int i = startIndex + 1; i < items.Count; i++)
        {
            if (items[i].Value < items[outputIndex].Value)
            {
                outputIndex = i;
            }
        }

        return outputIndex;
    }
}
