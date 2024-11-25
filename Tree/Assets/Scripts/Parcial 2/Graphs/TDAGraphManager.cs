using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TestGraphs
{
    public class TDAGraphManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private GameObject player;
        [SerializeField] private TMP_Text weightText;
        [SerializeField] private int totalWeight;

        [Header("Lines")]
        [SerializeField] private Material lineMaterial;
        [SerializeField] private GameObject arrowObject;
        [SerializeField] private Transform content;

        [Header("Lists Nodos y conexiones")]
        private List<NodeGraphVisual> visualNodesGraph;
        [SerializeField] private List<ConnectionGraphVisual> connectionNodesGraph;

        private NodeGraph startNode;
        private TDADynamicGraph<NodeGraph> dynamicNodesGraph;

        [SerializeField] int indexNodes;

        private void Awake()
        {
            indexNodes = 0;
            dynamicNodesGraph = new TDADynamicGraph<NodeGraph>();
            visualNodesGraph = new List<NodeGraphVisual>();
        }

        private void Start()
        {
            NodeGraphVisual[] nodes = GetComponentsInChildren<NodeGraphVisual>();

            foreach (NodeGraphVisual nodeVisual in nodes)
            {
                nodeVisual.weight = indexNodes;
                indexNodes++;
            }

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
                    weightText.text = "Travel Weight: " + totalWeight;
                }
            }
            else
            {
                Debug.Log("No hay camino hacia " + clickedNode.nodeName);
            }
        }

        //Algoritmo Dijkstra
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
            foreach (ConnectionGraphVisual connection in connectionNodesGraph)
            {
                CreateConnection(connection.fromNode, connection.toNode, connection.weight);
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
                Vector3 posFrom = fromVisual.transform.position;
                Vector3 posTo = toVisual.transform.position;

                Debug.DrawLine(posFrom, posTo, Color.black, 10f);
            }
        }
    }
}