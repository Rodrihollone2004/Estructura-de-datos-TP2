using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthMaker : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private float delayPlayerTravel;

    [Header("Lines")]
    [SerializeField] private Material lineMaterial;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private Transform content;

    [Header("Lists Nodos y conexiones")]
    [SerializeField] private List<NodeLabyrinthMaker> visualNodesLabyrinth;

    public NodeLabyrinthMaker StartNode { get; set; }
    public NodeLabyrinthMaker ExitNode { get; set; }

    [SerializeField] bool isExit;
    [SerializeField] int indexNodes;

    private NodeGraph currentNode;
    private TDADynamicLabyrinth<NodeGraph> dynamicNodesLabyrinth;

    private void Awake()
    {
        indexNodes = 0;
        dynamicNodesLabyrinth = new TDADynamicLabyrinth<NodeGraph>();
        visualNodesLabyrinth = new List<NodeLabyrinthMaker>();
    }

    private void Start()
    {
        NodeLabyrinthMaker[] nodes = GetComponentsInChildren<NodeLabyrinthMaker>();

        foreach (NodeLabyrinthMaker nodeVisual in nodes)
        {
            nodeVisual.weight = indexNodes;
            indexNodes++;
        }

        for (int i = 0; i < nodes.Length; i++)
        {
            visualNodesLabyrinth.Add(nodes[i]);
        }

        for (int i = 0; i < visualNodesLabyrinth.Count; i++)
        {
            NodeGraph node = new NodeGraph(visualNodesLabyrinth[i].nodeName, visualNodesLabyrinth[i].weight);
            dynamicNodesLabyrinth.Add(node);
        }

        ShowConnections();
    }

    private void Update()
    {
        if (StartNode != null)
        {
            currentNode = dynamicNodesLabyrinth.GetElement(StartNode.weight);
            player.transform.position = visualNodesLabyrinth[currentNode.weight].transform.position;
        }

        if (isExit && ExitNode != null)
        {
            SearchExitPath(ExitNode);
            isExit = false;
        }
        else if (isExit && ExitNode == null)
        {
            Debug.Log("Tenga un nodo de salida seleccionado");
            isExit = false;
        }
    }

    public void SearchExitPath(NodeLabyrinthMaker ExitNode)
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

    public void ShowConnections()
    {
        float maxDistance = 2f;

        for (int i = 0; i < visualNodesLabyrinth.Count; i++)
        {
            NodeLabyrinthMaker fromVisual = visualNodesLabyrinth[i];

            if (fromVisual.CompareTag("Wall"))
            {
                continue;
            }

            Vector3 fromPosition = fromVisual.transform.position;

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

            foreach (Vector3 direction in directions)
            {
                RaycastHit hit;
                if (Physics.Raycast(fromPosition, direction, out hit, maxDistance))
                {
                    NodeLabyrinthMaker toVisual = hit.collider.GetComponent<NodeLabyrinthMaker>();

                    if (toVisual != null && !hit.collider.CompareTag("Wall"))
                    {
                        int weight = 1;
                        CreateConnection(fromVisual, toVisual, weight);

                        CreateConnection(toVisual, fromVisual, weight);
                    }
                }
            }
        }
    }

    public void RefreshConnections()
    {
        for (int i = 0; i < dynamicNodesLabyrinth.Cardinality(); i++)
        {
            dynamicNodesLabyrinth.ClearConnections(dynamicNodesLabyrinth.GetElement(i));
        }

        foreach (Transform child in content)
        {
            if (child.name.StartsWith("ConnectionLine") || child.name.StartsWith("Arrow"))
            {
                Destroy(child.gameObject);
            }
        }

        ShowConnections(); 

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


    private void CreateConnection(NodeLabyrinthMaker fromVisual, NodeLabyrinthMaker toVisual, int weight)
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
        }
    }
}
