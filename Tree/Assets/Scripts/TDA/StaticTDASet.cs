using UnityEngine;

public class StaticTDASet : TDA
{
    private int[] elements;
    private int maxSize;
    private int actualSize;

    public StaticTDASet(int maxSize)
    {
        this.maxSize = maxSize;
        elements = new int[this.maxSize];
        actualSize = 0;
    }

    public override bool Add(int element)
    {
        if (actualSize >= maxSize || Contains(element))
            return false;

        elements[actualSize++] = element;
        return true;
    }

    public override bool Remove(int element)
    {
        for (int i = 0; i < actualSize; i++)
        {
            if (elements[i] == element)
            {
                for (int j = i; j < actualSize - 1; j++)
                {
                    elements[j] = elements[j + 1];
                }
                actualSize--;
                return true;
            }
        }

        return false;
    }

    public override void Show()
    {
        if (!isEmpty())
        {
            string showValues = "";
            for (int i = 0; i < elements.Length; i++)
            {
                showValues += elements[i].ToString() + ", ";
            }

            Debug.Log("Los números del conjunto son: " + showValues);
        }
    }

    public override int Cardinality()
    {
        return elements[actualSize];
    }

    public override bool isEmpty()
    {
        if (elements.Length == 0)
        {
            return true;
        }

        return false;
    }

    public override bool Contains(int element)
    {
        for (int i = 0; i < actualSize; i++)
        {
            if (elements[i] == element)
                return true;
        }

        return false;
    }

    public override TDA Union(TDA otherSet)
    {
        StaticTDASet resultSet = new StaticTDASet(maxSize + otherSet.Cardinality());
        foreach (int item in elements)
        {
            resultSet.Add(item);
        }

        for (int i = 0; i < otherSet.Cardinality(); i++)
        {
            int otherElement = otherSet.GetElement(i);
            resultSet.Add(otherElement);
        }
        return resultSet;
    }

    public override TDA Intersection(TDA otherSet)
    {
        StaticTDASet resultSet = new StaticTDASet(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (otherSet.Contains(elements[i]))
            {
                resultSet.Add(elements[i]);
            }
        }
        return resultSet;
    }

    public override TDA Difference(TDA otherSet)
    {
        StaticTDASet resultSet = new StaticTDASet(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (!otherSet.Contains(elements[i]))
            {
                resultSet.Add(elements[i]);
            }
        }
        return resultSet;
    }

    public override int GetElement(int index)
    {
        if (index < 0 || index >= actualSize)
            Debug.Log("No está");
        return elements[index];
    }
}