using System.Collections.Generic;
using UnityEngine;

public class TDAGraphManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private int totalWeight;

    [Header("Lines")]
    [SerializeField] private Material lineMaterial;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private Transform content;

    [Header("Lists Nodos y conexiones")]
    private List<NodeGraphVisual> visualNodesGraph;

    private NodeGraph startNode;
    private TDADynamicGraph<NodeGraph> dynamicNodesGraph;

    private void Awake()
    {
        dynamicNodesGraph = new TDADynamicGraph<NodeGraph>();
        visualNodesGraph = new List<NodeGraphVisual>();
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

        startNode = dynamicNodesGraph.GetElement(0);
        player.transform.position = visualNodesGraph[startNode.weight].transform.position;

        ShowConnections();

        Debug.Log(dynamicNodesGraph.Cardinality());
    }

    public void OnNodeClicked(NodeGraphVisual clickedNode)
    {
        NodeGraph destinationNode = dynamicNodesGraph.GetElement(clickedNode.weight);

        List<NodeGraph> path = FindShortestPath(startNode, destinationNode);

        if (path != null)
        {
            startNode = destinationNode;
            player.transform.position = visualNodesGraph[destinationNode.weight].transform.position;

            for (int i = 0; i < path.Count - 1; i++)
            {
                totalWeight += dynamicNodesGraph.GetWeight(path[i], path[i + 1]);
            }
        }
        else
        {
            Debug.Log("No hay camino hacia " + clickedNode.nodeName);
        }
    }

    public List<NodeGraph> FindShortestPath(NodeGraph start, NodeGraph target)
    {
        List<(NodeGraph nodeGraph, int cost)> priorityList = new List<(NodeGraph, int)>();
        Dictionary<NodeGraph, int> travelWeight = new Dictionary<NodeGraph, int>();
        Dictionary<NodeGraph, NodeGraph> previousNodes = new Dictionary<NodeGraph, NodeGraph>();

        foreach (NodeGraph node in dynamicNodesGraph.GetAllNodes())
        {
            travelWeight[node] = int.MaxValue;
            previousNodes[node] = null;
        }

        travelWeight[start] = 0;
        priorityList.Add((start, 0));

        while (priorityList.Count > 0)
        {
            priorityList.Sort((a, b) => a.cost.CompareTo(b.cost));
            NodeGraph currentNode = priorityList[0].nodeGraph;
            priorityList.RemoveAt(0);

            if (currentNode.Equals(target))
            {
                return ConstructPath(previousNodes, target);
            }

            List<(NodeGraph, int)> connections = dynamicNodesGraph.GetConnectionsFromNode(currentNode);

            foreach ((NodeGraph, int) connection in connections)
            {
                NodeGraph neighbor = connection.Item1;
                int weight = connection.Item2;
                int lineWeight = travelWeight[currentNode] + weight;

                if (lineWeight < travelWeight[neighbor])
                {
                    travelWeight[neighbor] = lineWeight;
                    previousNodes[neighbor] = currentNode;

                    if (!priorityList.Exists(neighborNode => neighborNode.nodeGraph.Equals(neighbor)))
                    {
                        priorityList.Add((neighbor, lineWeight));
                    }
                }
            }
        }

        return null;
    }
    private List<NodeGraph> ConstructPath(Dictionary<NodeGraph, NodeGraph> previousNodes, NodeGraph target)
    {
        List<NodeGraph> path = new List<NodeGraph>();
        for (NodeGraph dest = target; dest != null; dest = previousNodes[dest])
        {
            path.Add(dest);
        }
        path.Reverse();
        return path;
    }


    private void ShowConnections()
    {
        if (dynamicNodesGraph.Cardinality() > 0)
        {
            CreateConnection(visualNodesGraph[0], visualNodesGraph[1], 8);
            CreateConnection(visualNodesGraph[0], visualNodesGraph[2], 2);

            CreateConnection(visualNodesGraph[1], visualNodesGraph[3], 4);
            CreateConnection(visualNodesGraph[1], visualNodesGraph[4], 9);

            CreateConnection(visualNodesGraph[2], visualNodesGraph[3], 3);
            CreateConnection(visualNodesGraph[2], visualNodesGraph[5], 5);

            CreateConnection(visualNodesGraph[3], visualNodesGraph[7], 3);

            CreateConnection(visualNodesGraph[4], visualNodesGraph[9], 7);

            CreateConnection(visualNodesGraph[5], visualNodesGraph[6], 1);
            CreateConnection(visualNodesGraph[5], visualNodesGraph[7], 2);

            CreateConnection(visualNodesGraph[6], visualNodesGraph[8], 9);
            CreateConnection(visualNodesGraph[6], visualNodesGraph[10], 15);

            CreateConnection(visualNodesGraph[7], visualNodesGraph[8], 6);

            CreateConnection(visualNodesGraph[8], visualNodesGraph[10], 11);

            CreateConnection(visualNodesGraph[9], visualNodesGraph[8], 1);
            CreateConnection(visualNodesGraph[9], visualNodesGraph[11], 8);

            CreateConnection(visualNodesGraph[10], visualNodesGraph[11], 4);
        }
    }
    private void CreateConnection(NodeGraphVisual fromVisual, NodeGraphVisual toVisual, int weight)
    {
        NodeGraph fromNode = dynamicNodesGraph.GetElement(visualNodesGraph.IndexOf(fromVisual));
        NodeGraph toNode = dynamicNodesGraph.GetElement(visualNodesGraph.IndexOf(toVisual));

        if (dynamicNodesGraph.Contains(fromNode)
            && dynamicNodesGraph.Contains(toNode)
            && dynamicNodesGraph.AddConnection(fromNode, toNode, weight))
        {
            GameObject lineObj = new GameObject("ConnectionLine");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineObj.transform.SetParent(content);
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            Vector3 posFrom = fromVisual.transform.position;
            Vector3 posTo = toVisual.transform.position;
            lineRenderer.SetPosition(0, posFrom);
            lineRenderer.SetPosition(1, posTo);

            if (arrowObject != null)
            {
                Vector3 direction = (posTo - posFrom).normalized;
                float arrowOffset = 0.7f;
                Vector3 arrowPosition = posTo - direction * arrowOffset;

                GameObject arrowHead = Instantiate(arrowObject, arrowPosition, Quaternion.LookRotation(direction));
                arrowHead.transform.Rotate(0, 0, 90);
                arrowHead.transform.SetParent(content);
            }

            ConnectionGraphVisual connection = lineObj.AddComponent<ConnectionGraphVisual>();
            connection.conectName = $"{fromVisual.nodeName} - {toVisual.nodeName}";
            connection.weight = weight;
        }
    }
}