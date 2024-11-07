using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDALabyrinthManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private float delayPlayerTravel;

    [Header("Lines")]
    [SerializeField] private Material lineMaterial;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private Transform content;

    [Header("Lists Nodos y conexiones")]
    [SerializeField] private List<NodeGraphVisual> visualNodesLabyrinth;

    [SerializeField] private NodeGraphVisual startNode;
    [SerializeField] private NodeGraphVisual exitNode;

    [SerializeField] bool isExit;

    private NodeGraph currentNode;
    private TDADynamicLabyrinth<NodeGraph> dynamicNodesLabyrinth;

    private void Awake()
    {
        dynamicNodesLabyrinth = new TDADynamicLabyrinth<NodeGraph>();
        visualNodesLabyrinth = new List<NodeGraphVisual>();
    }

    private void Start()
    {
        NodeGraphVisual[] nodes = GetComponentsInChildren<NodeGraphVisual>();
        for (int i = 0; i < nodes.Length; i++)
        {
            visualNodesLabyrinth.Add(nodes[i]);
        }

        for (int i = 0; i < visualNodesLabyrinth.Count; i++)
        {
            NodeGraph node = new NodeGraph(visualNodesLabyrinth[i].nodeName, visualNodesLabyrinth[i].weight);
            dynamicNodesLabyrinth.Add(node);
        }

        currentNode = dynamicNodesLabyrinth.GetElement(startNode.weight);
        player.transform.position = visualNodesLabyrinth[currentNode.weight].transform.position;

        ShowConnections();

        Debug.Log(dynamicNodesLabyrinth.Cardinality());
    }

    private void Update()
    {
        if (isExit)
        {
            isExit = false;
            SearchExitPath(exitNode);
        }
    }

    public void SearchExitPath(NodeGraphVisual ExitNode)
    {
        NodeGraph destinationNode = dynamicNodesLabyrinth.GetElement(ExitNode.weight);

        List<NodeGraph> path = FindPathToExit(currentNode, destinationNode);

        if (path != null)
        {
            foreach (NodeGraph node in path)
            {
                Debug.Log(node.name);  
            }

            StartCoroutine(MovePlayerAlongPath(path));
        }
        else
        {
            Debug.Log("No hay camino hacia " + ExitNode.nodeName);
        }
    }

    public void ActivatePlayer()
    {
        isExit = true;
    }


    public List<NodeGraph> FindPathToExit(NodeGraph start, NodeGraph target)
    {
        Queue<NodeGraph> queue = new Queue<NodeGraph>();  
        Dictionary<NodeGraph, NodeGraph> previousNodes = new Dictionary<NodeGraph, NodeGraph>();
        List<NodeGraph> visitedNodes = new List<NodeGraph>();  

        queue.Enqueue(start);
        visitedNodes.Add(start);
        previousNodes[start] = null;

        while (queue.Count > 0)
        {
            NodeGraph currentNode = queue.Dequeue();

            if (currentNode.Equals(target))
            {
                return ConstructPath(previousNodes, target);
            }

            List<(NodeGraph, int)> connections = dynamicNodesLabyrinth.GetConnectionsFromNode(currentNode);

            foreach ((NodeGraph, int) connection in connections)
            {
                if (!visitedNodes.Contains(connection.Item1))
                {
                    visitedNodes.Add(connection.Item1);
                    queue.Enqueue(connection.Item1);
                    previousNodes[connection.Item1] = currentNode;
                }
            }
        }

        return null;
    }

    private IEnumerator MovePlayerAlongPath(List<NodeGraph> path)
    {
        foreach (NodeGraph node in path)
        {
            Debug.Log("Nodo: " + node.name);  
            yield return new WaitForSeconds(delayPlayerTravel);

            player.transform.position = visualNodesLabyrinth[node.weight].transform.position;
        }

        currentNode = path[path.Count - 1];

        Debug.Log("Listo");
    }

    private List<NodeGraph> ConstructPath(Dictionary<NodeGraph, NodeGraph> previousNodes, NodeGraph target)
    {
        List<NodeGraph> path = new List<NodeGraph>();
        NodeGraph current = target;

        while (current != null)
        {
            path.Add(current);
            current = previousNodes[current];
        }

        path.Reverse();

        return path; 
    }

    private void ShowConnections()
    {
        if (dynamicNodesLabyrinth.Cardinality() > 0)
        {
            CreateConnection(visualNodesLabyrinth[0], visualNodesLabyrinth[6], 1);
            CreateConnection(visualNodesLabyrinth[6], visualNodesLabyrinth[0], 1);

            CreateConnection(visualNodesLabyrinth[0], visualNodesLabyrinth[1], 1);
            CreateConnection(visualNodesLabyrinth[1], visualNodesLabyrinth[0], 1);


            CreateConnection(visualNodesLabyrinth[1], visualNodesLabyrinth[7], 1);
            CreateConnection(visualNodesLabyrinth[7], visualNodesLabyrinth[1], 1);

            CreateConnection(visualNodesLabyrinth[2], visualNodesLabyrinth[3], 1);
            CreateConnection(visualNodesLabyrinth[3], visualNodesLabyrinth[2], 1);

            CreateConnection(visualNodesLabyrinth[2], visualNodesLabyrinth[8], 1);
            CreateConnection(visualNodesLabyrinth[8], visualNodesLabyrinth[2], 1);

            CreateConnection(visualNodesLabyrinth[3], visualNodesLabyrinth[4], 1);
            CreateConnection(visualNodesLabyrinth[4], visualNodesLabyrinth[3], 1);

            CreateConnection(visualNodesLabyrinth[7], visualNodesLabyrinth[8], 1);
            CreateConnection(visualNodesLabyrinth[8], visualNodesLabyrinth[7], 1);

            CreateConnection(visualNodesLabyrinth[8], visualNodesLabyrinth[9], 1);
            CreateConnection(visualNodesLabyrinth[9], visualNodesLabyrinth[8], 1);

            CreateConnection(visualNodesLabyrinth[9], visualNodesLabyrinth[10], 1);
            CreateConnection(visualNodesLabyrinth[10], visualNodesLabyrinth[9], 1);

            CreateConnection(visualNodesLabyrinth[9], visualNodesLabyrinth[15], 1);
            CreateConnection(visualNodesLabyrinth[15], visualNodesLabyrinth[9], 1);

            CreateConnection(visualNodesLabyrinth[11], visualNodesLabyrinth[5], 1);
            CreateConnection(visualNodesLabyrinth[5], visualNodesLabyrinth[11], 1);

            CreateConnection(visualNodesLabyrinth[12], visualNodesLabyrinth[18], 1);
            CreateConnection(visualNodesLabyrinth[18], visualNodesLabyrinth[12], 1);

            CreateConnection(visualNodesLabyrinth[13], visualNodesLabyrinth[19], 1);
            CreateConnection(visualNodesLabyrinth[19], visualNodesLabyrinth[13], 1);

            CreateConnection(visualNodesLabyrinth[13], visualNodesLabyrinth[14], 1);
            CreateConnection(visualNodesLabyrinth[14], visualNodesLabyrinth[13], 1);

            CreateConnection(visualNodesLabyrinth[14], visualNodesLabyrinth[15], 1);
            CreateConnection(visualNodesLabyrinth[15], visualNodesLabyrinth[14], 1);

            CreateConnection(visualNodesLabyrinth[14], visualNodesLabyrinth[20], 1);
            CreateConnection(visualNodesLabyrinth[20], visualNodesLabyrinth[14], 1);

            CreateConnection(visualNodesLabyrinth[16], visualNodesLabyrinth[17], 1);
            CreateConnection(visualNodesLabyrinth[17], visualNodesLabyrinth[16], 1);

            CreateConnection(visualNodesLabyrinth[17], visualNodesLabyrinth[11], 1);
            CreateConnection(visualNodesLabyrinth[11], visualNodesLabyrinth[17], 1);

            CreateConnection(visualNodesLabyrinth[17], visualNodesLabyrinth[23], 1);
            CreateConnection(visualNodesLabyrinth[23], visualNodesLabyrinth[17], 1);

            CreateConnection(visualNodesLabyrinth[18], visualNodesLabyrinth[19], 1);
            CreateConnection(visualNodesLabyrinth[19], visualNodesLabyrinth[18], 1);

            CreateConnection(visualNodesLabyrinth[20], visualNodesLabyrinth[21], 1);
            CreateConnection(visualNodesLabyrinth[21], visualNodesLabyrinth[20], 1);

            CreateConnection(visualNodesLabyrinth[20], visualNodesLabyrinth[26], 1);
            CreateConnection(visualNodesLabyrinth[26], visualNodesLabyrinth[20], 1);

            CreateConnection(visualNodesLabyrinth[22], visualNodesLabyrinth[16], 1);
            CreateConnection(visualNodesLabyrinth[16], visualNodesLabyrinth[22], 1);

            CreateConnection(visualNodesLabyrinth[23], visualNodesLabyrinth[29], 1);
            CreateConnection(visualNodesLabyrinth[29], visualNodesLabyrinth[23], 1);

            CreateConnection(visualNodesLabyrinth[24], visualNodesLabyrinth[25], 1);
            CreateConnection(visualNodesLabyrinth[25], visualNodesLabyrinth[24], 1);

            CreateConnection(visualNodesLabyrinth[25], visualNodesLabyrinth[26], 1);
            CreateConnection(visualNodesLabyrinth[26], visualNodesLabyrinth[25], 1);

            CreateConnection(visualNodesLabyrinth[26], visualNodesLabyrinth[27], 1);
            CreateConnection(visualNodesLabyrinth[27], visualNodesLabyrinth[26], 1);

            CreateConnection(visualNodesLabyrinth[27], visualNodesLabyrinth[28], 1);
            CreateConnection(visualNodesLabyrinth[28], visualNodesLabyrinth[27], 1);

            CreateConnection(visualNodesLabyrinth[28], visualNodesLabyrinth[22], 1);
            CreateConnection(visualNodesLabyrinth[22], visualNodesLabyrinth[28], 1);
        }
    }
    private void CreateConnection(NodeGraphVisual fromVisual, NodeGraphVisual toVisual, int weight)
    {
        NodeGraph fromNode = dynamicNodesLabyrinth.GetElement(visualNodesLabyrinth.IndexOf(fromVisual));
        NodeGraph toNode = dynamicNodesLabyrinth.GetElement(visualNodesLabyrinth.IndexOf(toVisual));

        if (dynamicNodesLabyrinth.Contains(fromNode)
            && dynamicNodesLabyrinth.Contains(toNode)
            && dynamicNodesLabyrinth.AddConnection(fromNode, toNode, weight))
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

