using UnityEngine;

public class TreeMono : MonoBehaviour
{
    [SerializeField] private int[] myArray = { 2, 1, 8, 0, 5, 9, 4, 3 };

    private ShowNodes showNodes;

    private Tree tree; //Árbol ABB
    private AVLTree treeAVL; //Árbol AVL

    private Vector3 startPosNodes;

    [Header("Distancia entre los nodos")]
    [SerializeField] private float offSetY;
    [SerializeField] private float offSetX;

    private void Awake()
    {
        tree = new Tree();
        treeAVL = new AVLTree();

        showNodes = FindObjectOfType<ShowNodes>();
        startPosNodes = new Vector3(0, 6f, 0);
    }

    private void Start()
    {
        //for (int i = 0; i < myArray.Length; i++)
        //{
        //    tree.InsertValue(myArray[i]); // // Valores del Árbol ABB (se muestra el árbol ordenado sin balancearse ni nada) --Ejercicio 1--
        //}

        //for (int i = 0; i < myArray.Length; i++)
        //{
        //    treeAVL.Insert(myArray[i]); // Valores del Árbol AVL (se balancean los nodos según el FE) --Ejercicio 2--
        //}

        //showNodes.ShowOrderNodes(tree.Root, startPosNodes, offSetX, offSetY); // Mostrar los nodos del --Ejercicio 1--
        //showNodes.ShowOrderNodes(treeAVL.Root, startPosNodes, offSetX, offSetY); // Mostrar los nodos del --Ejercicio 2--

        //Debug.Log("Altura: " + tree.Altura()); // Sirve para ambos ejercicios, calcula la altura máxima
    }
}
