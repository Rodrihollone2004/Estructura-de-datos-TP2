﻿using TMPro;
using UnityEngine;

public class TreeMono : MonoBehaviour
{
    private int[] myArray = { 2, 1, 8, 0, 5, 9, 4, 3, 100, 101, 102, 103, 5, 5, 5 };

    private ShowNodes showNodes;

    private Tree tree; //Árbol ABB
    private AVLTree treeAVL; //Árbol AVL

    private Vector3 startPosNodes;

    [Header("Distancia entre los nodos")]
    [SerializeField] private Transform content;
    [SerializeField] private float offSetY;
    [SerializeField] private float offSetX;

    [SerializeField] private bool push = false;
    [SerializeField] private int num = 0;

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
        //showNodes.ShowOrderNodes(tree.Root, startPosNodes, offSetX, offSetY, content); // Mostrar los nodos del --Ejercicio 1--

        for (int i = 0; i < myArray.Length; i++)
        {
            treeAVL.Insert(myArray[i]); // Valores del Árbol AVL (se balancean los nodos según el FE) --Ejercicio 2--
        }
        Print();

        Debug.Log("Height: " + tree.Height()); // Sirve para ambos ejercicios, calcula la altura máxima
    }


    private void Update()
    {
        if (push)
        {
            push = false;
            treeAVL.InsertValue(num);
            Print();
        }
    }

    void Print()
    {
        Transform[] childs = GetComponentsInChildren<Transform>();
        for (int i = 0; i < childs.Length; i++)
            if (childs[i] != transform)
                Destroy(childs[i].gameObject);

        showNodes.ShowOrderNodes(treeAVL.Root, startPosNodes, offSetX, offSetY, content);
    }
}