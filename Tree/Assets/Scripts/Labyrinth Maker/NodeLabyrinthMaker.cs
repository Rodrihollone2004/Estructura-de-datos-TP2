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
        switch (inputsMaker.Operations)
        {
            case InputsMaker.OperationsMaker.Start:
                labyrinthMaker.StartNode = this;
                UnityEngine.Debug.Log("start");
                break;
            case InputsMaker.OperationsMaker.End:
                labyrinthMaker.ExitNode = this;
                UnityEngine.Debug.Log("end");
                break;
            case InputsMaker.OperationsMaker.Wall:
                this.gameObject.tag = "Wall";
                labyrinthMaker.RefreshConnections();
                UnityEngine.Debug.Log("wall");
                break;
            case InputsMaker.OperationsMaker.Floor:
                this.gameObject.tag = "Floor";
                labyrinthMaker.RefreshConnections();
                UnityEngine.Debug.Log("floor");
                break;
            default:
                this.gameObject.tag = "Floor";
                labyrinthMaker.RefreshConnections();
                break;
        }
    }
}
