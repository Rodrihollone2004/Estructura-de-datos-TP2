using UnityEngine;

public class AVLTree : ABBTree
{
    public void Insert(int value)
    {
        Root = InsertValue(value, Root);
    }

    public int GetBalanceFactor(NodeABB node)
    {
        int fe = 0;
        if (node != null)
        {
            int rightHeight = node.Right != null ? CalculateHeight(node.Right) : 0;
            int leftHeight = node.Left != null ? CalculateHeight(node.Left) : 0;
            fe = leftHeight - rightHeight;
        }

        return fe;
    }

    public NodeABB LeftRotate(NodeABB node)
    {
        Debug.Log("Left Rotation in node: " + node.Value);

        NodeABB newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;

        CalculateHeight(node);
        CalculateHeight(newRoot);

        return newRoot;
    }

    public NodeABB DoubleLeftRotate(NodeABB node)
    {
        Debug.Log("Double Left Rotation");
        node.Right = RightRotate(node.Right);
        return LeftRotate(node);
    }

    public NodeABB RightRotate(NodeABB node)
    {
        Debug.Log("Right Rotation in node: " + node.Value);
        NodeABB newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;

        CalculateHeight(node);
        CalculateHeight(newRoot);
       
        return newRoot;
    }

    public NodeABB DoubleRightRotate(NodeABB node)
    {
        Debug.Log("Double Right Rotation");
        node.Left = LeftRotate(node.Left);
        return RightRotate(node);
    }

    protected override NodeABB InsertValue(int value, NodeABB node)
    {
        if (node == null)
        {
            return new NodeABB(value);
        }
        if (value < node.Value)
        {
            node.Left = InsertValue(value, node.Left);
        }
        else if (value > node.Value)
        {
            node.Right = InsertValue(value, node.Right);
        }

        CalculateHeight(node);
        
        int fe = GetBalanceFactor(node);

        if (fe > 1 && value < (node.Left?.Value ?? 0))
            return RightRotate(node);

        if (fe < -1 && value > (node.Right?.Value ?? 0))
            return LeftRotate(node);

        if (fe > 1 && value > (node.Left?.Value ?? 0))
            return DoubleRightRotate(node);

        if (fe < -1 && value < (node.Right?.Value ?? 0))
            return DoubleLeftRotate(node);

        return node;
    }
}
