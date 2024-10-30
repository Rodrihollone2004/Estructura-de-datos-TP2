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
    public override T GetElement(int index)
    {
        throw new System.NotImplementedException();
    }
}