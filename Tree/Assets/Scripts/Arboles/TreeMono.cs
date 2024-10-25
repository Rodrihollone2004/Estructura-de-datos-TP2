using UnityEngine;

public class TreeMono : MonoBehaviour
{
    [SerializeField] private int[] myArray = { 2, 1, 8, 0, 5, 9, 4, 3 }; 

    private ShowNodes showNodes;
    private AVLTree treeAVL;
    private Tree tree;

    private Vector3 startPosNodes;

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
        //    tree.InsertValue(myArray[i]);
        //}

        for (int i = 0; i < myArray.Length; i++)
        {
            treeAVL.Insert(myArray[i]);
        }

        //showNodes.ShowOrderNodes(tree.Root, startPosNodes, offSetX, offSetY); // 1
        showNodes.ShowOrderNodes(treeAVL.Root, startPosNodes, offSetX, offSetY); // 2

        //Debug.Log("Altura: " + tree.Altura());
    }
}
