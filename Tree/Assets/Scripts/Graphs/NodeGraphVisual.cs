using UnityEngine;

public class NodeGraphVisual : MonoBehaviour
{
    public string nodeName = "";
    public int weight = 0;

    private void OnMouseDown()
    {
        TestGraphs.TDAGraphManager graphManager = FindObjectOfType<TestGraphs.TDAGraphManager>();
        if (graphManager != null)
        {
            graphManager.OnNodeClicked(this);
        }
    }
}
