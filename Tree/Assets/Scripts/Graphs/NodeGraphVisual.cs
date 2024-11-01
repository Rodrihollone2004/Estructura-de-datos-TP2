using UnityEngine;

public class NodeGraphVisual : MonoBehaviour
{
    public string nodeName = "";
    public int weight = 0;

    private void OnMouseDown()
    {
        TDAGraphManager graphManager = FindObjectOfType<TDAGraphManager>();
        if (graphManager != null)
        {
            graphManager.OnNodeClicked(this);
        }
    }
}
