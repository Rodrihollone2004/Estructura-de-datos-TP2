using System.Collections.Generic;
using UnityEngine;

public class TDAGraphManager : MonoBehaviour
{
    [Header("Lines")]
    [SerializeField] private Material lineMaterial;

    [Header("Lists Nodos y conexiones")]
    List<NodeGraphVisual> visualNodesGraph;
    [SerializeField] List<ConnectionGraphVisual> visualConectionGraph; 

    TDADynamicGraph<NodeGraph> dynamicNodesGraph;

    private void Awake()
    {
        dynamicNodesGraph = new TDADynamicGraph<NodeGraph>();
        visualNodesGraph = new List<NodeGraphVisual>();
        visualConectionGraph = new List<ConnectionGraphVisual>();
    }

    private void Start()
    {
        NodeGraphVisual[] nodes = GetComponentsInChildren<NodeGraphVisual>();
        for (int i = 0; i < nodes.Length; i++)
        {
            visualNodesGraph.Add(nodes[i]);
        }

        for (int i = 0; i < visualNodesGraph.Count; i++)
        {
            NodeGraph node = new NodeGraph(visualNodesGraph[i].nodeName, visualNodesGraph[i].weight);
            dynamicNodesGraph.Add(node);
        }

        Debug.Log(dynamicNodesGraph.Cardinality());
    }

    private void DrawLine(Vector3 start, Vector3 end, Transform content)
    {
        GameObject lineObject = new GameObject("Line");
        lineObject.transform.SetParent(content);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}