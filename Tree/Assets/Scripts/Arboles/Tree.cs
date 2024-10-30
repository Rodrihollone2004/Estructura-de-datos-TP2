using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public NodeABB Root { get; set; }

    public void InsertValue(int value)
    {
        if (Root == null)
        {
            Root = new NodeABB(value);
        }
        else
        {
            InsertValue(value, Root);
        }
    }

    protected virtual NodeABB InsertValue(int value, NodeABB node)
    {
        if (value < node.Value && node.Left == null)
        {
            node.Left = new NodeABB(value);
        }
        else if (value < node.Value && node.Left != null)
        {
            return InsertValue(value, node.Left);
        }

        if (value > node.Value && node.Right == null)
        {
            node.Right = new NodeABB(value);
        }
        else if (value > node.Value && node.Right != null)
        {
            return InsertValue(value, node.Right);
        }

        return node;
    }

    public int Height() => CalculateHeight(Root);

    protected int CalculateHeight(NodeABB node)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            node.Height = 1 + Mathf.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
            return node.Height;
        }
    }

    public void PreOrder(NodeABB aBB)
    {
        if (aBB != null)
        {
            Debug.Log("Pre Order: " + aBB.Value.ToString());
            PreOrder(aBB.Left);
            PreOrder(aBB.Right);
        }
    }

    public void InOrder(NodeABB aBB)
    {
        if (aBB != null)
        {
            InOrder(aBB.Left);
            Debug.Log("In Order: " + aBB.Value.ToString());
            InOrder(aBB.Right);
        }
    }

    public void PostOrder(NodeABB aBB)
    {
        if (aBB != null)
        {
            PostOrder(aBB.Left);
            PostOrder(aBB.Right);
            Debug.Log("Post Order: " + aBB.Value.ToString());
        }
    }

    public void LevelOrder(NodeABB node)
    {
        Queue<NodeABB> nodesQueue = new Queue<NodeABB>();

        nodesQueue.Enqueue(node);

        while (nodesQueue.Count > 0)
        {
            node = nodesQueue.Dequeue();

            if (node.Left != null) { nodesQueue.Enqueue(node.Left); }

            if (node.Right != null) { nodesQueue.Enqueue(node.Right); }
        }
    }
}
