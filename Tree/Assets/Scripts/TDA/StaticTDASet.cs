using System;
using TMPro;
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
        if(actualSize >= maxSize || Contains(element))
            return false;

        elements[actualSize++] = element;
        return true;
    }

    public override bool Remove(int element)
    {
        if (Contains(element))
        {
            int lastNumber = elements[actualSize];
            elements[element] = lastNumber;
            actualSize--;

            return true;
        }

        return false;
    }

    public override void Show()
    {
        string showValues = "";
        for (int i = 0; i < elements.Length; i++)
        {
            showValues += elements[i].ToString() + ", ";
        }

        Debug.Log("Los números del conjunto son: ");
    }

    public override int Cardinality()
    {
        return elements[actualSize];
    }

    public override bool isEmpty()
    {
        if(elements.Length == 0)
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
        StaticTDASet resultSet = new StaticTDASet(maxSize + otherSet.Cardinality()); // Nuevo conjunto con capacidad suficiente
        foreach (int item in elements)
        {
            resultSet.Add(item); // Agrega elementos del conjunto actual
        }
        for (int i = 0; i < otherSet.Cardinality(); i++)
        {
            int otherElement = otherSet.GetElementAt(i);
            resultSet.Add(otherElement); // Agrega elementos del otro conjunto
        }
        return resultSet;
    }

    public override TDA Intersection(TDA otherSet)
    {
        StaticTDASet resultSet = new StaticTDASet(maxSize); // Nuevo conjunto con capacidad suficiente
        for (int i = 0; i < actualSize; i++)
        {
            if (otherSet.Contains(elements[i])) // Agrega solo los elementos que están en ambos conjuntos
            {
                resultSet.Add(elements[i]);
            }
        }
        return resultSet;
    }

    public override TDA Difference(TDA otherSet)
    {
        StaticTDASet resultSet = new StaticTDASet(maxSize); // Nuevo conjunto con capacidad suficiente
        for (int i = 0; i < actualSize; i++)
        {
            if (!otherSet.Contains(elements[i])) // Agrega solo los elementos que no están en el otro conjunto
            {
                resultSet.Add(elements[i]);
            }
        }
        return resultSet;
    }

    public override int GetElementAt(int index)
    {
        if (index < 0 || index >= actualSize)
            throw new IndexOutOfRangeException("Índice fuera de rango.");
        return elements[index];
    }
}