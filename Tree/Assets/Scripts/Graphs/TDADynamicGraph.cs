using System;
using System.Collections.Generic;

public class TDADynamicGraph<T> : TDA<T>
{
    Dictionary<T, List<(T, int)>> nodes;
    int size = 0;
    public TDADynamicGraph()
    {
        nodes = new Dictionary<T, List<(T, int)>>();
    }

    public override bool Add(T element)
    {
        if (Contains(element))
            return false;

        List<(T, int)> list = new List<(T, int)>();
        nodes.Add(element, list);
        size++;
        return true;
    }

    public bool AddConnection(T from, T to, int weight)
    {
        if (!Contains(from) || !Contains(to))
            return false;

        foreach ((T, int) connection in nodes[from])
        {
            if (connection.Item1.Equals(to))
                return false;
        }

        nodes[from].Add((to, weight));
        return true;
    }

    public override int Cardinality() => size;

    public override bool Contains(T element) => nodes.ContainsKey(element);

    public override bool IsEmpty() => nodes.Count == 0;

    public override bool Remove(T element)
    {
        if (nodes.ContainsKey(element))
        {
            nodes.Remove(element);

            size--;
            return true;
        }
        
        return false;
    }

    public override T GetElement(int index)
    {
        if (index < 0 || index >= size)
            throw new IndexOutOfRangeException("Índice fuera de rango en GetElement.");

        foreach (T node in nodes.Keys)
        {
            if (index == 0)
                return node;
            index--;
        }

        throw new IndexOutOfRangeException("No se encontró un elemento para el índice dado.");
    }

    public List<(T, int)> GetConnectionsFromNode(T from)
    {
        if (!nodes.ContainsKey(from))
            return null;

        return nodes[from];
    }

    public override string Show()
    {
        throw new System.NotImplementedException();
    }

    public override TDA<T> Union(TDA<T> otherSet)
    {
        throw new System.NotImplementedException();
    }
    public override TDA<T> Intersection(TDA<T> otherSet)
    {
        throw new System.NotImplementedException();
    }
    public override TDA<T> Difference(TDA<T> otherSet)
    {
        throw new System.NotImplementedException();
    }
}