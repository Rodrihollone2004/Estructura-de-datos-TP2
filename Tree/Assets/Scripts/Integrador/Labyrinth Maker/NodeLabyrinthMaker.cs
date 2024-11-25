using UnityEngine;

public class NodeLabyrinthMaker : NodeGraphVisual
{
    InputsMaker inputsMaker;
    LabyrinthMaker labyrinthMaker;

    private void Awake()
    {
        inputsMaker = FindObjectOfType<InputsMaker>();
        labyrinthMaker = FindObjectOfType<LabyrinthMaker>();
    }

    public override void OnMouseDown()
    {
        Renderer renderer = this.GetComponent<Renderer>();

        switch (inputsMaker.Operations)
        {
            case InputsMaker.OperationsMaker.Start:

                if (labyrinthMaker.StartNode == null)
                {
                    labyrinthMaker.StartNode = this;
                    renderer.material.color = Color.blue;
                    UnityEngine.Debug.Log("start");
                }
                else
                {
                    NodeLabyrinthMaker tempNode = labyrinthMaker.StartNode;
                    Renderer tempRenderer = tempNode.GetComponent<Renderer>();
                    labyrinthMaker.StartNode = this;
                    tempRenderer.material.color = Color.white;
                    renderer.material.color = Color.blue;
                    UnityEngine.Debug.Log("start");
                }
                break;
            case InputsMaker.OperationsMaker.End:
                if (labyrinthMaker.ExitNode == null)
                {
                    labyrinthMaker.ExitNode = this;
                    renderer.material.color = Color.red;
                    UnityEngine.Debug.Log("end");
                }
                else
                {
                    NodeLabyrinthMaker tempNode = labyrinthMaker.ExitNode;
                    Renderer tempRenderer = tempNode.GetComponent<Renderer>();
                    labyrinthMaker.ExitNode = this;
                    tempRenderer.material.color = Color.white;
                    renderer.material.color = Color.red;
                    UnityEngine.Debug.Log("end");
                }
                break;
            case InputsMaker.OperationsMaker.Wall:
                this.gameObject.tag = "Wall";
                renderer.material.color = Color.black;
                labyrinthMaker.RefreshConnections();
                UnityEngine.Debug.Log("wall");
                break;
            case InputsMaker.OperationsMaker.Floor:
                this.gameObject.tag = "Floor";
                renderer.material.color = Color.white;
                labyrinthMaker.RefreshConnections();
                UnityEngine.Debug.Log("floor");
                break;
        }
    }
}
