using TMPro;
using UnityEngine;

public class ShowNodes : MonoBehaviour
{
    [SerializeField] private GameObject nodeObject;
    [SerializeField] private Material lineMaterial;
    private TMP_Text nodeValue;
    private Transform transform;

    private void Awake()
    {
        nodeValue = nodeObject.GetComponentInChildren<TMP_Text>();
        transform = nodeObject.GetComponent<Transform>();
    }

    //Método para que se muestren los nodos en Unity
    public void ShowOrderNodes(NodeABB node, Vector3 position, float offSetX, float offSetY)
    {
        if (node != null)
        {
            nodeValue.text = node.Value.ToString();

            GameObject newNode = Instantiate(nodeObject, position, transform.rotation);

            if (node.Left != null)
            {
                Vector3 leftPosition = position + new Vector3(-offSetX, -offSetY, 0);
                ShowOrderNodes(node.Left, leftPosition, offSetX * 0.5f, offSetY);
                DrawLine(position, leftPosition);
            }
            if (node.Right != null)
            {
                Vector3 rightPosition = position + new Vector3(offSetX, -offSetY, 0);
                ShowOrderNodes(node.Right, rightPosition, offSetX * 0.5f, offSetY);
                DrawLine(position, rightPosition);
            }
        }
    }

    //Método para dibujar las líneas entre nodos
    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("Line");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
