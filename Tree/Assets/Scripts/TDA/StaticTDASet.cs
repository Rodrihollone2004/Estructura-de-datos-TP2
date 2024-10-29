using UnityEngine;

public class StaticTDASet<T> : TDA<T>
{
    private T[] elements;
    private int maxSize;
    private int actualSize;

    public StaticTDASet(int maxSize)
    {
        this.maxSize = maxSize;
        elements = new T[this.maxSize];
        actualSize = 0;
    }

    public override bool Add(T element)
    {
        if (actualSize >= maxSize || Contains(element))
            return false;

        elements[actualSize++] = element;
        return true;
    }

    public override bool Remove(T element)
    {
        for (int i = 0; i < actualSize; i++)
        {
            if (elements[i].Equals(element))
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

    public override bool Contains(T element)
    {
        for (int i = 0; i < actualSize; i++)
        {
            if (elements[i].Equals(element))
                return true;
        }

        return false;
    }

    public override TDA<T> Union(TDA<T> otherSet)
    {
        StaticTDASet<T> unionSet = new StaticTDASet<T>(maxSize + otherSet.Cardinality());
        foreach (T item in elements)
        {
            unionSet.Add(item);
        }

        for (int i = 0; i < otherSet.Cardinality(); i++)
        {
            T otherElement = otherSet.GetElement(i);
            unionSet.Add(otherElement);
        }
        return unionSet;
    }

    public override TDA<T> Intersection(TDA<T> otherSet)
    {
        StaticTDASet<T> intersectionSet = new StaticTDASet<T>(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (otherSet.Contains(elements[i]))
            {
                intersectionSet.Add(elements[i]);
            }
        }
        return intersectionSet;
    }

    public override TDA<T> Difference(TDA<T> otherSet)
    {
        StaticTDASet<T> differenceSet = new StaticTDASet<T>(maxSize);
        for (int i = 0; i < actualSize; i++)
        {
            if (!otherSet.Contains(elements[i]))
            {
                differenceSet.Add(elements[i]);
            }
        }

        for (int i = 0; i < otherSet.Cardinality(); i++)
        {
            T element = otherSet.GetElement(i);
            if (!Contains(element))
            {
                differenceSet.Add(element);
            }
        }
        return differenceSet;
    }

    public override T GetElement(int index)
    {
        if (index < 0 || index >= actualSize)
            Debug.Log("No number");
        return elements[index];
    }
}