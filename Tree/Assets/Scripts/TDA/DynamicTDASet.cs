using System.Collections.Generic;
using UnityEngine;

public class DynamicTDASet<T> : TDA<T>
{
    List<T> list;

    public DynamicTDASet() 
    { 
        list = new List<T>();
    }

    public override bool Add(T element)
    {
        if (Contains(element))
        {
            return false;
        }

        list.Add(element);
        return true;
    }

    public override bool Remove(T element)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(element))
            {
                list.Remove(element); 
                return true;
            }
        }

        return false;
    }

    public override bool Contains(T element)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(element))
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
        if (!IsEmpty())
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

    public override bool IsEmpty()
    {
        if (list.Count == 0)
            return true;

        return false;
    }

    public override TDA<T> Union(TDA<T> otherSet)
    {
        DynamicTDASet<T> unionSet = new DynamicTDASet<T>();
        foreach (T item in list)
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
        DynamicTDASet<T> intersectionSet = new DynamicTDASet<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (otherSet.Contains(list[i]))
            {
                intersectionSet.Add(list[i]);
            }
        }
        return intersectionSet;
    }

    public override TDA<T> Difference(TDA<T> otherSet)
    {
        DynamicTDASet<T> differenceSet = new DynamicTDASet<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (!otherSet.Contains(list[i]))
            {
                differenceSet.Add(list[i]);
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
        if (index < 0)
            Debug.Log("No number");

        return list[index];
    }
}
