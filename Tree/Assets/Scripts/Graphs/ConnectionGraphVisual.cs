using UnityEngine;

[System.Serializable]

public class ConnectionGraphVisual  
{
    public NodeGraphVisual fromNode;
    public NodeGraphVisual toNode;
    public string conectName = "";
    public int weight = 0;
}
