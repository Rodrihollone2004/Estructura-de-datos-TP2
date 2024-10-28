using System.Collections.Generic;
using UnityEngine;

public class DynamicTDASet : TDA
{
    List<int> list;

    public DynamicTDASet() 
    { 
        list = new List<int>();
    }

    public override bool Add(int element)
    {
        if (Contains(element))
        {
            return false;
        }

        list.Add(element);
        return true;
    }

    public override bool Remove(int element)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == element)
            {
                list.Remove(element); 
                return true;
            }
        }

        return false;
    }

    public override bool Contains(int element)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == element)
                return true;
        }

        return false;
    }

    public override int Cardinality()
    {
        return list.Count;
    }

    public override string Show()
    {
        if (!isEmpty())
        {
            string textDynamic = "";
            for (int i = 0; i < list.Count; i++)
            {
                textDynamic += list[i].ToString() + ", ";
            }

            return textDynamic;
        }

        return "";
    }

    public override bool isEmpty()
    {
        if (list.Count == 0)
            return true;

        return false;
    }

    public override TDA Union(TDA otherSet)
    {
        DynamicTDASet unionSet = new DynamicTDASet();
        foreach (int item in list)
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
        DynamicTDASet intersectionSet = new DynamicTDASet();
        for (int i = 0; i < list.Count; i++)
        {
            if (otherSet.Contains(list[i]))
            {
                intersectionSet.Add(list[i]);
            }
        }
        return intersectionSet;
    }

    public override TDA Difference(TDA otherSet)
    {
        DynamicTDASet differenceSet = new DynamicTDASet();
        for (int i = 0; i < list.Count; i++)
        {
            if (!otherSet.Contains(list[i]))
            {
                differenceSet.Add(list[i]);
            }
        }
        return differenceSet;
    }
    public override int GetElement(int index)
    {
        if (index < 0)
            Debug.Log("No number");

        return list[index];
    }
}
