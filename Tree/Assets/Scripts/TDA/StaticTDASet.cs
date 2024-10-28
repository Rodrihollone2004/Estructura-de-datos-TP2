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

    public override string Show()
    {
        if (!isEmpty())
        {
            string textStatic = "";
            for (int i = 0; i < actualSize; i++)
            {
                textStatic += elements[i].ToString() + ", ";
            }
        
            return textStatic;
        }

        return "";
    }

    public override int Cardinality()
    {
        return actualSize;
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
        StaticTDASet unionSet = new StaticTDASet(maxSize + otherSet.Cardinality());
        foreach (int item in elements)
        {
            unionSet.Add(item);
        }

        for (int i = 0; i < otherSet.Cardinality(); i++)
        {
            int otherElement = otherSet.GetElement(i);
            unionSet.Add(otherElement);
        }
        return unionSet;
    }

    public override TDA Intersection(TDA otherSet)
    {
        StaticTDASet intersectionSet = new StaticTDASet(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (otherSet.Contains(elements[i]))
            {
                intersectionSet.Add(elements[i]);
            }
        }
        return intersectionSet;
    }

    public override TDA Difference(TDA otherSet)
    {
        StaticTDASet differenceSet = new StaticTDASet(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (!otherSet.Contains(elements[i]))
            {
                differenceSet.Add(elements[i]);
            }
        }
        return differenceSet;
    }

    public override int GetElement(int index)
    {
        if (index < 0 || index >= actualSize)
            Debug.Log("No number");
        return elements[index];
    }
}