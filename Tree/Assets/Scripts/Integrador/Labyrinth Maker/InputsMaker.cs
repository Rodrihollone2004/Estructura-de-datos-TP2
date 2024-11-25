using UnityEngine;

public class InputsMaker : MonoBehaviour
{
    public enum OperationsMaker
    {
        Start,
        End,
        Floor,
        Wall
    }

    public OperationsMaker Operations { get; private set; }

    private void Update()
    {
        InputsOfMakerOperations();
    }

    private void InputsOfMakerOperations()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Operations = OperationsMaker.Start;
        
        if (Input.GetKeyDown(KeyCode.Alpha2)) Operations = OperationsMaker.End;

        if (Input.GetKeyDown(KeyCode.Alpha3)) Operations = OperationsMaker.Floor;

        if (Input.GetKeyDown(KeyCode.Alpha4)) Operations = OperationsMaker.Wall;
    }
}
