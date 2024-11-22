using TMPro;
using UnityEngine;

namespace TestTree
{
    public class TestTree : MonoBehaviour
    {
        [SerializeField] private int[] myArray = { 2, 1, 8, 0, 5, 9, 4, 3, 100, 101, 102, 103, 5, 5, 5 };

        private ShowNodes showNodes;

        private ABBTree treeABB; //Árbol ABB
        private AVLTree treeAVL; //Árbol AVL

        private Vector3 startPosNodes;

        [Header("Distancia entre los nodos")]
        [SerializeField] private Transform content;
        [SerializeField] private float offSetY;
        [SerializeField] private float offSetX;

        [Header("Actualizaciones en play")]
        [SerializeField] private TMP_Text actualHeight;
        [SerializeField] private bool push = false;
        [SerializeField] private int num = 0;

        private void Awake()
        {
            treeABB = new ABBTree();
            treeAVL = new AVLTree();

            showNodes = FindObjectOfType<ShowNodes>();
            startPosNodes = new Vector3(0, 6f, 0);
        }

        private void Start()
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                treeABB.InsertValue(myArray[i]);
            }

            for (int i = 0; i < myArray.Length; i++)
            {
                treeAVL.Insert(myArray[i]);
            }

            //Print(treeABB); //  -------- Ejercicio 1 (ABB Tree) --------
            Print(treeAVL); //  -------- Ejercicio 2 (AVL Tree) --------
        }


        private void Update()
        {
            if (push)
            {
                push = false;

                //treeABB.InsertValue(num); //  -------- Ejercicio 1 (ABB Tree) --------
                //Print(treeABB);

                treeAVL.InsertValue(num); //  -------- Ejercicio 2 (AVL Tree) --------
                Print(treeAVL);
            }
        }

        private void Print(ABBTree tree)
        {
            Transform[] childs = GetComponentsInChildren<Transform>();
            for (int i = 0; i < childs.Length; i++)
                if (childs[i] != transform)
                    Destroy(childs[i].gameObject);

            showNodes.ShowOrderNodes(tree.Root, startPosNodes, offSetX, offSetY, content);
            actualHeight.text = "Height: " + tree.Height();
        }
    }
}